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
[Authorize(Roles = "Member")]
public class OrdersController : ControllerBase
{
    private readonly StoreContext _context;

    private readonly UserManager<User> _userManager;


    public OrdersController(StoreContext context, UserManager<User> userManager)
    {
        _context = context;

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
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        // Todo: Get details from future settings table
        invoice.Sender = new InvoiceSender()
        {
            Address = "1234 Main St",
            City = "New York",
            Company = "Company Name",
            Zip = "123456",
            Country = "USA",
        };
        invoice.BottomNotice = "Thank you for your business. Please make sure all payments are made within 2 weeks.";
        invoice.DueDate = DateTime.UtcNow.AddDays(14);
        invoice.IssueDate = DateTime.UtcNow;
        invoice.Number = "INV-000" + invoice.IssueDate.Date.ToString("yyyy-MM-dd") + "-" + invoice.Id;
        invoice.Logo = "https://via.placeholder.com/150";
        invoice.OrderItems = orderItems; 

        // TODO: get from settings
        // invoice.Settings = new InvoiceSettings(){
        //     Currency = "MUR",
        //     Format = "A4",
        //     Height = "210mm",
        //     Width = "297mm",
        //     Locale = "en-US",
        //     MarginBottom = 10,
        //     MarginLeft = 10,
        //     MarginRight = 10,
        //     MarginTop = 10,
        //     TaxNotation = "vat", 
        // };
        User client = user;
        invoice.UserId = client.Id; 
        _context.Invoices.Add(invoice);  
        return invoice.Id;
    } 
}