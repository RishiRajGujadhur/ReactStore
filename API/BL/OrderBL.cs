using System.Security.Claims;
using System.Text.Json;  
using Store.Infrastructure.Entities;
using Store.Infrastructure.Entities.OrderAgrgregate;
using API.Extensions; 
using API.Hubs;
using API.Integrations.Services.Kafka;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data.DTOs.Order;
using Store.Infrastructure.Data.DTOs.Account;
using Store.Infrastructure.Data;

namespace API.BL
{
    public interface IOrderBL
    {
        Task<List<OrderDto>> GetOrders(ClaimsPrincipal User);
        Task<List<OrderDto>> GetAllOrders();
        Task<List<OrderDto>> GetOrdersByUser(UserDto userDto);
        Task SetOrderStatus(OrderStatusDto orderStatusDto, ClaimsPrincipal User);
        Task<OrderDto> GetOrder(int id, ClaimsPrincipal User);
        Task<(Order, bool)> CreateOrder(CreateOrderDto orderDto, ClaimsPrincipal User);
    }

    public class OrderBL : IOrderBL
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly StoreContext _context;
        private readonly ILogger<OrderBL> _logger; 
        private readonly ProducerService _producerService;
        public UserManager<User> _userManager { get; }

        public OrderBL(StoreContext context, UserManager<User> userManager, ILogger<OrderBL> logger, ProducerService producerService, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _producerService = producerService;
            _hubContext = hubContext;
        }

        public async Task<List<OrderDto>> GetOrders(ClaimsPrincipal User)
        {
            var orders = await _context.Orders
            .ProjectOrderToOrderDto()
            .Where(x => x.BuyerId == User.Identity.Name)
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync();

            return orders;
        }

        public async Task<List<OrderDto>> GetAllOrders()
        {
            var orders = await _context.Orders
           .ProjectOrderToOrderDto()
           .OrderByDescending(x => x.OrderDate)
           .ToListAsync();

            return orders;
        }

        public async Task<List<OrderDto>> GetOrdersByUser(UserDto userDto)
        {
            var orders = await _context.Orders
           .Where(x => x.BuyerId == userDto.UserName)
           .ProjectOrderToOrderDto()
           .OrderByDescending(x => x.OrderDate)
           .ToListAsync();

            return orders;
        }

        public async Task SetOrderStatus(OrderStatusDto orderStatusDto, ClaimsPrincipal User)
        {
            try
            {
                var order = await _context.Orders
                .Where(x => x.Id == orderStatusDto.Id)
                .FirstOrDefaultAsync() ?? throw new Exception("Order not found");

                order.LastModifiedTimestamp =  DateTime.UtcNow; 
                order.LastModifiedUserName = User.Identity.Name;
                
                SetOrderStatus(orderStatusDto, order);
                string orderJson = JsonSerializer.Serialize(order);
                await _producerService.ProduceAsync("OrderStatusChangedTopic", orderJson);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex.Message);
                // Return a 500 Internal Server Error status code
                throw new Exception(ex.InnerException.Message);
            }

            static void SetOrderStatus(OrderStatusDto orderStatusDto, Order order)
            {
                order.OrderStatus = orderStatusDto.OrderStatus switch
                {
                    "PaymentReceived" => OrderStatus.PaymentReceived,
                    "PaymentFailed" => OrderStatus.PaymentFailed,
                    "Pending" => OrderStatus.Pending,
                    "Delivered" => OrderStatus.Delivered,
                    _ => order.OrderStatus // Default case, keep the existing order status
                };
            }
        }

        public async Task<OrderDto> GetOrder(int id, ClaimsPrincipal User)
        {
            return await _context.Orders
              .ProjectOrderToOrderDto()
              .Where(x => x.BuyerId == User.Identity.Name && x.Id == id)
              .FirstOrDefaultAsync();
        }

        public async Task<(Order, bool)> CreateOrder(CreateOrderDto orderDto, ClaimsPrincipal User)
        {
            var basket = await _context.Baskets
            .RetrieveBasketWithItems(User.Identity.Name)
            .FirstOrDefaultAsync() ?? throw new Exception("Basket not found");

            var items = new List<OrderItem>();
            await AddOrderItems(basket, items);

            var subtotal = items.Sum(item => item.Price * item.Quantity);
            var deliveryFee = subtotal > 10000 ? 0 : 500;
            await CreateInvoice(items, User);

            Order order = CreateOrderObject(orderDto, User, items, subtotal, deliveryFee);

            _context.Orders.Add(order);
            _context.Baskets.Remove(basket);
            await SaveAddress(orderDto, User);
            var result = await _context.SaveChangesAsync() > 0;

            await SendNotification(); 
       
            return (order, result);

            async Task SendNotification()
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Order created");
            }
        }

        #region Private Methods
        private static Order CreateOrderObject(CreateOrderDto orderDto, ClaimsPrincipal User, List<OrderItem> items, long subtotal, int deliveryFee)
        {
            return new Order
            {
                CreatedAtTimestamp = DateTime.UtcNow, 
                CreatedByUserName = User.Identity.Name,
                OrderItems = items,
                BuyerId = User.Identity.Name,
                ShippingAddress = orderDto.ShippingAddress,
                Subtotal = subtotal,
                DeliveryFee = deliveryFee,
                PaymentMethod = orderDto.PaymentMethod,
            };
        }

        private async Task AddOrderItems(Basket basket, List<OrderItem> items)
        {
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
        }

        private async Task SaveAddress(CreateOrderDto orderDto, ClaimsPrincipal User)
        {
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
        }

        private async Task<int> CreateInvoice(List<OrderItem> orderItems, ClaimsPrincipal User)
        {

            GeneralSettings generalSettings = await _context.GeneralSettings.FirstOrDefaultAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            // INVOICE sender cannot be the same as the user who purchases the product thus I have changed the sender to the last sender in the database
            // This is a temporary solution until I have a proper way to handle this in the future when there are multiple sellers 
            // For multi vendors, I will associate the vendorId to the product at the time of creating the product in the inventory, each vendor will have their own inventory.
            User client = user;
            var invoiceSettings = _context.InvoiceSettings.OrderBy(i => i.Id).FirstOrDefault();

            Invoice invoice = new Invoice
            {
                CreatedByUserId = user.Id,
                CreatedByUserName = user.UserName,
                BottomNotice = invoiceSettings.BottomNotice,
                DueDate = DateTime.UtcNow.AddDays(14),
                IssueDate = DateTime.UtcNow,
                CreatedAtTimestamp = DateTime.UtcNow,
                Logo = generalSettings?.LogoURL,
                OrderItems = orderItems,
                Settings = invoiceSettings,
                UserId = client.Id,
                Sender = _context.InvoiceSenders.OrderByDescending(x => x.Id).FirstOrDefault()
            };
            invoice.Number = "INV-000" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "-" + invoice.Id;
            _context.Invoices.Add(invoice);
            return invoice.Id;
        }

        #endregion
    }
}