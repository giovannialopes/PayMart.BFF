using Microsoft.AspNetCore.Mvc;

namespace PayMart.API.Core.Controllers.Clients;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllClient(
        [FromServices] HttpClient httpClient)
    {
        var response = await httpClient.GetStringAsync("https://localhost:5001/api/Client");
        return Ok(response);
    }

}
