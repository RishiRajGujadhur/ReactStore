using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IAccountBL
    {
        Task<UserDto> Login(LoginDto loginDto);
        Task<UserAddress> GetSavedAddress();
        Task<List<User>> GetAllUsers();
        Task RegisterUser(RegisterDto registerDto);
        Task<UserDto> GetCurrentUser();
    }

    public class AccountBL : IAccountBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public AccountBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<UserDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddress> GetSavedAddress()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetCurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}