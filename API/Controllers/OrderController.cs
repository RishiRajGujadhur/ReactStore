using API.BL;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderBL _orderBL;

    public OrdersController(IOrderBL orderBL)
    {
        _orderBL = orderBL;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetOrders()
    {
        var orders = await _orderBL.GetOrders(User);
        return orders;
    }

    [HttpGet("listAllOrders")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
    {
        var orders = await _orderBL.GetAllOrders();
        return orders;

    }

    [Authorize(Roles = "Admin")]
    [HttpPost("GetOrdersByUser")]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersByUser(UserDto userDto)
    {
        var orders = await _orderBL.GetOrdersByUser(userDto);
        return orders;
    }

    [HttpPut]
    public async Task<IActionResult> SetOrderStatus(OrderStatusDto orderStatusDto)
    {
        await _orderBL.SetOrderStatus(orderStatusDto, User);
        return NoContent();

    }


    [HttpGet("{id}", Name = "GetOrder")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        var order = await _orderBL.GetOrder(id, User);
        return order;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto orderDto)
    {
        var (order, result) = await _orderBL.CreateOrder(orderDto, User);

        if (result) return CreatedAtRoute("GetOrder", new { id = order.Id }, order.Id);

        return BadRequest("Problem creating order");
    }
}