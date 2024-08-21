using Microsoft.AspNetCore.Mvc;
using PayMart.Application.Core.NovaPasta;
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
}
