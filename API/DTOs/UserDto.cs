namespace API.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public BasketDto Basket { get; set; }
}