using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayMart.Application.Core.NovaPasta;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Client;
using PayMart.Infrastructure.Core.Services;
using System.Runtime.Serialization;
using System.Text.Json;

namespace PayMart.API.Core.Controllers.Clients;

[Route("api/")]
[ApiController]
public class ClientController : ControllerBase
{

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllClient(
    [FromServices] HttpClient http)
    {
        var response = await http.GetStringAsync(ServicesURL.Client("getAll"));
        return Ok(JsonFormatter.Formatter(response));
    }

    [HttpGet("getID")]
    public async Task<IActionResult> GetIDClient(
    [FromServices] HttpClient http, 
    [FromHeader] int id)
    {
        var response = await http.GetStringAsync(ServicesURL.Client("getID", id));
        return Ok(JsonFormatter.Formatter(response));
    }

    [HttpPost("Post")]
    public async Task<IActionResult> PostClient(
    [FromServices] HttpClient http,
    [FromBody] RequestPostClient request)
    {
        var httpResponse = await http.PostAsJsonAsync(ServicesURL.Client("post"), request);

        if (httpResponse.IsSuccessStatusCode)
        {
            var response = new ResponsePostClient { Name = request.Name, Email = request.Email, Age = request.Age };
            return Created("", response);
        }

        return BadRequest();
    }


}
