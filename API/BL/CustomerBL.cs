using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface ICustomerBL
    {
        UserManager<User> UserManager { get; }
        IMapper Mapper { get; } 
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task<Customer> CreateCustomer(CreateCustomerDto customerDTO);
        Task UpdateCustomer(int id, Customer customer);
        Task DeleteCustomer(int id);
    }

    public class CustomerBL : ICustomerBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public UserManager<User> UserManager => throw new NotImplementedException();

        public IMapper Mapper => throw new NotImplementedException();

        public CustomerBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<IEnumerable<Customer>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> CreateCustomer(CreateCustomerDto customerDTO)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomer(int id, Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}