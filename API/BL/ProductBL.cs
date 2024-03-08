using System.Security.Claims;
using API.Data; 
using API.Extensions;
using API.RequestHelpers;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;
using Store.Infrastructure.Data.DTOs.Product;
using Store.Infrastructure.Entities;

namespace API.BL
{
    public interface IProductBL
    {
        Task<PagedList<Product>> GetProducts([FromQuery] ProductParams productParams);
        Task<Product> GetProduct(int id);
        Task<object> GetFilters();
        Task<(Product, bool)> CreateProduct([FromForm] CreateProductDto productDto, ClaimsPrincipal user);
        Task<(Product,bool)> UpdateProduct([FromForm] UpdateProductDto productDto, ClaimsPrincipal user);
        Task<bool> DeleteProduct(int id);
    }

    public class ProductBL : IProductBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }
        private readonly IMapper _mapper;
            private readonly ImageService _imageService;

        public ProductBL(StoreContext context, UserManager<User> userManager, IMapper mapper, ImageService imageService)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<PagedList<Product>> GetProducts([FromQuery] ProductParams productParams)
        {
            var query = _context.Products
                .Sort(productParams.OrderBy)
                .Search(productParams.SearchTerm)
                .Filter(productParams.Brands, productParams.Types)
                .AsQueryable();

            var products =
                await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);
            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id) ?? throw new KeyNotFoundException("Product not found");
            return product;
        }

        public async Task<object> GetFilters()
        {
            var brands = await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
            var types = await _context.Products.Select(p => p.Type).Distinct().ToListAsync();
            return new { brands, types };
        }

        public async Task<(Product, bool)> CreateProduct([FromForm] CreateProductDto productDto, ClaimsPrincipal User)
        {
            var product = _mapper.Map<Product>(productDto);
            
            product.CreatedAtTimestamp = DateTime.UtcNow;
            product.CreatedByUserName = User.Identity.Name;
            
            await UploadFile(productDto, product);

            _context.Products.Add(product);

            var result = await _context.SaveChangesAsync() > 0;

            return (product, result);
        }

        public async Task<(Product,bool)> UpdateProduct([FromForm] UpdateProductDto productDto, ClaimsPrincipal User)
        {
            var product = await _context.Products.FindAsync(productDto.Id) ?? throw new KeyNotFoundException("Product not found");
            
            _mapper.Map(productDto, product);
            product.LastModifiedTimestamp = DateTime.UtcNow;
            product.LastModifiedUserName = User.Identity.Name;
            
            await UpdateFile(productDto, product); 

            var result = await _context.SaveChangesAsync() > 0;

            return (product, result);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) throw new Exception("Product not found");

            if (!string.IsNullOrEmpty(product.PublicId))
                await _imageService.DeleteImageAsync(product.PublicId);

            _context.Products.Remove(product);

            var result = await _context.SaveChangesAsync() > 0;

            return result;
        }

        #region Helpers
        private async Task UpdateFile(UpdateProductDto productDto, Product product)
        {
            if (productDto.File != null)
            {
                var imageUploadResult = await _imageService.AddImageAsync(productDto.File);

                if (imageUploadResult.Error != null)
                    throw new Exception(imageUploadResult.Error.Message);

                if (!string.IsNullOrEmpty(product.PublicId))
                    await _imageService.DeleteImageAsync(product.PublicId);

                product.PictureUrl = imageUploadResult.SecureUrl.ToString();
                product.PublicId = imageUploadResult.PublicId;
            }
        }

        private async Task UploadFile(CreateProductDto productDto, Product product)
        {
            if (productDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(productDto.File);
                
                if (imageResult.Error != null) throw new Exception(imageResult.Error.Message);
                
                product.PictureUrl = imageResult.SecureUrl.ToString();
                
                product.PublicId = imageResult.PublicId;
            }
        }
        #endregion
    }
}