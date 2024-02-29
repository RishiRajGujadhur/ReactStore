using API.Data;
using API.DTOs;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Member")]
public class PaymentsController : ControllerBase
{ 
    private readonly StoreContext _context; 
    public PaymentsController( StoreContext context)
    { 
        _context = context; 
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent()
    {
        var basket = await _context.Baskets
            .RetrieveBasketWithItems(User.Identity.Name)
            .FirstOrDefaultAsync();

        if (basket == null) return NotFound();  
        // basket.PaymentIntentId = "basket.PaymentIntentId ?? intent.Id";
        // basket.ClientSecret = "basket.ClientSecret ?? intent.ClientSecret";

        _context.Update(basket);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest(new ProblemDetails { Title = "Problem updating basket with intent" });

        return basket.MapBasketToDto();
    } 
}