using Store.Infrastructure.Data.DTOs.Basket;

namespace Store.Infrastructure.Data.DTOs.Account;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public BasketDto Basket { get; set; }
}