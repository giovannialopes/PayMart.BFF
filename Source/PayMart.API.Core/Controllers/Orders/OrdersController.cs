using Microsoft.AspNetCore.Mvc;
using PayMart.Application.Core.NovaPasta;
using PayMart.Domain.Core.Request.Order;
using PayMart.Domain.Core.Request.Product;
using PayMart.Domain.Core.Response.Order;
using PayMart.Domain.Core.Response.Product;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Orders;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(ReponsePostProduct), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll(
       [FromServices] HttpClient http)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Order("getAll"));

        if (httpResponse != null)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return NoContent();
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(typeof(ReponsePostProduct), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetID(
        [FromServices] HttpClient http,
        [FromHeader] int id)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Order("getID", id));

        if (httpResponse != null)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return NoContent();
    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(typeof(ReponsePostProduct), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Post(
        [FromServices] HttpClient http,
        [FromBody] RequestPostOrder request)
    {
        var httpResponse = await http.PostAsJsonAsync(ServicesURL.Product("post"), request);

        if (httpResponse.IsSuccessStatusCode)
        {
            var response = new ResponsePostOrder { Name = request.Name, Date = request.Date };

            return Created("", response);
        }

        return NoContent();
    }
}
