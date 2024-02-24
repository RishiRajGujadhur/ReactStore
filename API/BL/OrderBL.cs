using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IOrderBL
    {
        Task<List<OrderDto>> GetOrders();
        Task<List<OrderDto>> GetAllOrders();
        Task<List<OrderDto>> GetOrdersByUser(UserDto userDto);
        Task SetOrderStatus(OrderStatusDto orderStatusDto);
        Task<OrderDto> GetOrder(int id);
        Task<Order> CreateOrder(CreateOrderDto orderDto);
    }

    public class OrderBL : IOrderBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public OrderBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<List<OrderDto>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDto>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDto>> GetOrdersByUser(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task SetOrderStatus(OrderStatusDto orderStatusDto)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> CreateOrder(CreateOrderDto orderDto)
        {
            throw new NotImplementedException();
        }
    }
}