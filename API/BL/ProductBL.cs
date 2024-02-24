using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.RequestHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IProductBL
    {
        Task<PagedList<Product>> GetProducts([FromQuery] ProductParams productParams);
        Task<Product> GetProduct(int id);
        Task GetFilters();
        Task<Product> CreateProduct([FromForm] CreateProductDto productDto);
        Task<Product> UpdateProduct([FromForm] UpdateProductDto productDto);
        Task DeleteProduct(int id);
    }

    public class ProductBL : IProductBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public ProductBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<PagedList<Product>> GetProducts([FromQuery] ProductParams productParams)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetFilters()
        {
            throw new NotImplementedException();
        }

        public Task<Product> CreateProduct([FromForm] CreateProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct([FromForm] UpdateProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}