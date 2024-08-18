using Microsoft.AspNetCore.Mvc;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Clients;

[Route("api/[controller]")]
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

}
