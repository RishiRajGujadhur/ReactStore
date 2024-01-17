using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public StoreContext Context { get; }
        public ProductsController(StoreContext context)
        {
            this.Context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            return await Context.Products.ToListAsync();
        }

        [HttpGet("{id}")] // api/products/2
        public async Task<ActionResult<Product>> GetProductById(int Id){
             return await Context.Products.FindAsync(Id);
        }
    }
}