using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.API.BL;
using Store.API.Extentions;
using Store.API.Integrations.Services;
using Store.Infrastructure.Data;
using Store.Infrastructure.Data.DTOs.Account;
using Store.Infrastructure.Entities;
namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly StoreContext _context;
        private readonly IAccountBL _accountBL;

        public AccountController(UserManager<User> userManager, TokenService tokenService, StoreContext context, IAccountBL accountBL)
        {
            _context = context;
            _tokenService = tokenService;
            _userManager = userManager;
            _accountBL = accountBL;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            var userBasket = await BasketExtensions.RetrieveBasket(loginDto.Username, Response, _context);
            var anonBasket = await BasketExtensions.RetrieveBasket(Request.Cookies["buyerId"], Response, _context);

            if (anonBasket != null)
            {
                if (userBasket != null) _context.Baskets.Remove(userBasket);
                anonBasket.BuyerId = user.UserName;
                Response.Cookies.Delete("buyerId");
                await _context.SaveChangesAsync();
            }

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                Basket = anonBasket != null ? anonBasket.MapBasketToDto() : userBasket?.MapBasketToDto()
            };
        }

        [Authorize]
        [HttpGet("savedAddress")]
        public async Task<ActionResult<UserAddress>> GetSavedAddress()
        {
            return await _accountBL.GetSavedAddress(User);
        }

        [Authorize]
        [HttpGet("getAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _accountBL.GetAllUsers();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterDto registerDto)
        {
            var user = new User { UserName = registerDto.Username, Email = registerDto.Email };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, "Member");

            return StatusCode(201);
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            return await _accountBL.GetCurrentUser(User, Response);
        }

    }
}