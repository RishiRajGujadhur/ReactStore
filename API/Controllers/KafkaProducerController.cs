using System.Text.Json;
using API.Data;
using API.Entities;
using InventoryProducer.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaProducerController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly ProducerService _producerService;

        public KafkaProducerController(StoreContext Context, IConfiguration configuration, ProducerService producerService)
        {
            _context = Context;
            _producerService = producerService;
        }

        [HttpGet]
        public async Task<ActionResult<Inventory>> CreateInventoryItemProcessKafkaEvent()
        {
            var product = _context.Products.FirstOrDefault();
            Inventory inventoryItem = new() { InventoryID = 1, Product = product, ProductID = product.Id, CreatedAtTimestamp = DateTime.Now, StockQuantity = 1, CreatedByUserName = "test" };
            var message = JsonSerializer.Serialize(inventoryItem);
            await _producerService.ProduceAsync("AddInventory", message);
            return Ok(inventoryItem);
        }
    }
}