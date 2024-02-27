using API.BL;
using API.DTOs;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketBL _basketBl;

    public BasketController(IBasketBL basketBl)
    {
        _basketBl = basketBl;
    }

    [HttpGet(Name = "GetBasket")]
    public async Task<ActionResult<BasketDto>> GetBasket()
    {
        var basket = await _basketBl.GetBasket(User, Response);

        return Ok(basket);
    }

    [HttpPost]
    public async Task<ActionResult> AddItemToBasket(int productId, int quantity = 1)
    {
        var (result, basket) = await _basketBl.AddItemToBasket(productId, User, Response, quantity);
       
        if (result) return CreatedAtRoute("GetBasket", basket.MapBasketToDto());

        return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
    }
 
    [HttpDelete]
    public async Task<ActionResult> RemoveBasketItem(int productId, int quantity = 1)
    {
        var result = await _basketBl.RemoveBasketItem(productId, User, Response, quantity);

        if (result) return Ok();

        return BadRequest(new ProblemDetails { Title = "Problem removing item from the basket" });
    }  
}