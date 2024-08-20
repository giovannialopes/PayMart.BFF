using Microsoft.AspNetCore.Mvc;
using PayMart.Application.Core.NovaPasta;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Products;

[Route("api/[Controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll(
        [FromServices] HttpClient http)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Product("getAll"));

        if (httpResponse != null)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return NoContent();
    }

    [HttpGet]
    [Route("GetID")]
    public IActionResult GetID()
    {
        return Ok();
    }

    [HttpPost]
    [Route("Post")]
    public IActionResult Post()
    {
        return Ok();
    }

    [HttpPut]
    [Route("Update")]
    public IActionResult Update()
    {
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public IActionResult Delete()
    {
        return Ok();
    }

}
