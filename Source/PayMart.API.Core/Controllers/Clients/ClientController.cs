using Microsoft.AspNetCore.Mvc;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Clients;

[Route("api/")]
[ApiController]
public class ClientController : ControllerBase
{

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllClient(
        [FromServices] HttpClient request)
    {
        var response = await request.GetStringAsync(ServicesURL.ClientUrl);
        return Ok(response);
    }

    [HttpGet("getID")]
    public async Task<IActionResult> GetIDClient(
    [FromServices] HttpClient request, 
    [FromHeader] int id)
    {
        var response = await request.GetStringAsync(ServicesURL.Client("getID", id));
        return Ok(response);
    }



    
}
