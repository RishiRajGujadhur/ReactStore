using API.Data;
using API.DTOs;
using API.Entities;
using API.Entities.OrderAggregate;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")] 
public class OrdersController : ControllerBase
{
    private readonly StoreContext _context;
    private readonly ILogger<OrdersController> _logger;
    private readonly UserManager<User> _userManager;


    public OrdersController(StoreContext context, UserManager<User> userManager, ILogger<OrdersController> logger)
    {
        _context = context;
        _logger = logger;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetOrders()
    {
        var orders = await _context.Orders
            .ProjectOrderToOrderDto()
            .Where(x => x.BuyerId == User.Identity.Name)
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync();

        return orders;
    }

    [HttpGet("listAllOrders")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
    {
        var orders = await _context.Orders
            .ProjectOrderToOrderDto()
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync();

        return orders;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("GetOrdersByUser")]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersByUser(UserDto userDto)
    {
        var orders = await _context.Orders
            .Where(x => x.BuyerId == userDto.UserName)
            .ProjectOrderToOrderDto()
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync();

        return orders;
    }

    [HttpPut]
    public async Task<IActionResult> SetOrderStatus(OrderStatusDto orderStatusDto)
    {
        try
        {
            var order = await _context.Orders
            .Where(x => x.Id == orderStatusDto.Id)
            .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = orderStatusDto.OrderStatus switch
            {
                "PaymentReceived" => OrderStatus.PaymentReceived,
                "PaymentFailed" => OrderStatus.PaymentFailed,
                "Pending" => OrderStatus.Pending,
                "Delivered" => OrderStatus.Delivered,
                _ => order.OrderStatus // Default case, keep the existing order status
            };

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, AddErrorDetails(ex, "An error occurred while updating invoice settings."));
            // Return a 500 Internal Server Error status code
            return StatusCode(500, "Internal server error");
        }
        return NoContent();
    }


    [HttpGet("{id}", Name = "GetOrder")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        return await _context.Orders
            .ProjectOrderToOrderDto()
            .Where(x => x.BuyerId == User.Identity.Name && x.Id == id)
            .FirstOrDefaultAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto orderDto)
    {
        var basket = await _context.Baskets
            .RetrieveBasketWithItems(User.Identity.Name)
            .FirstOrDefaultAsync(); 
            
        if (basket == null) return BadRequest(new ProblemDetails { Title = "Could not locate basket" });

        var items = new List<OrderItem>();

        foreach (var item in basket.Items)
        {
            var productItem = await _context.Products.FindAsync(item.ProductId);
            var itemOrdered = new ProductItemOrdered
            {
                ProductId = productItem.Id,
                Name = productItem.Name,
                PictureUrl = productItem.PictureUrl
            };
            var orderItem = new OrderItem
            {
                ItemOrdered = itemOrdered,
                Price = productItem.Price,
                Quantity = item.Quantity,
            };
            items.Add(orderItem);
            productItem.QuantityInStock -= item.Quantity;
        }

        var subtotal = items.Sum(item => item.Price * item.Quantity);
        var deliveryFee = subtotal > 10000 ? 0 : 500;
        await CreateInvoice(items);
        var order = new Order
        {
            OrderItems = items,
            BuyerId = User.Identity.Name,
            ShippingAddress = orderDto.ShippingAddress,
            Subtotal = subtotal,
            DeliveryFee = deliveryFee,
            PaymentMethod = orderDto.PaymentMethod,
        };

        _context.Orders.Add(order);
        _context.Baskets.Remove(basket);

        if (orderDto.SaveAddress)
        {
            var user = await _context.Users.
                Include(a => a.Address)
                .FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);

            var address = new UserAddress
            {
                FullName = orderDto.ShippingAddress.FullName,
                Address1 = orderDto.ShippingAddress.Address1,
                Address2 = orderDto.ShippingAddress.Address2,
                City = orderDto.ShippingAddress.City,
                State = orderDto.ShippingAddress.State,
                Zip = orderDto.ShippingAddress.Zip,
                Country = orderDto.ShippingAddress.Country
            };
            user.Address = address;
        }

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return CreatedAtRoute("GetOrder", new { id = order.Id }, order.Id);

        return BadRequest("Problem creating order");
    }

    private async Task<int> CreateInvoice(List<OrderItem> orderItems)
    {
        Invoice invoice = new();
        GeneralSettings generalSettings = await _context.GeneralSettings.FirstOrDefaultAsync();
        var user = await _userManager.FindByNameAsync(User.Identity.Name); 
        invoice.Sender = _context.InvoiceSenders.Where(i => i.UserId == user.Id).FirstOrDefault(); 
        var invoiceSettings = _context.InvoiceSettings.OrderBy(i=>i.Id).FirstOrDefault();            
        invoice.BottomNotice = invoiceSettings.BottomNotice;
        invoice.DueDate = DateTime.UtcNow.AddDays(14);
        invoice.IssueDate = DateTime.UtcNow;
        invoice.Number = "INV-000" + invoice.IssueDate.Date.ToString("yyyy-MM-dd") + "-" + invoice.Id;
        invoice.Logo = generalSettings?.Logo;
        invoice.OrderItems = orderItems;
        invoice.Settings = invoiceSettings;
        User client = user;
        invoice.UserId = client.Id;
        _context.Invoices.Add(invoice);
        return invoice.Id;
    }

    private string AddErrorDetails(Exception ex, string message = "")
    {
        return message + " " + User.Identity.Name + " : " + DateTime.UtcNow.ToString() + " " + ex.Message + " " + ex.InnerException.Message + " " + ex.InnerException.InnerException.Message;
    }
}