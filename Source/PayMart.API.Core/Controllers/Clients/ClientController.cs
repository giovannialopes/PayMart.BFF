using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayMart.Application.Core.NovaPasta;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Client;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Clients;

[Route("api/")]
[ApiController]
public class ClientController : ControllerBase
{

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllClient(
    [FromServices] HttpClient http)
    {
        var response = await http.GetStringAsync(ServicesURL.Client("getAll"));
        return Ok(JsonFormatter.Formatter(response));
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetIDClient(
    [FromServices] HttpClient http, 
    [FromHeader] int id)
    {
        var response = await http.GetStringAsync(ServicesURL.Client("getID", id));
        return Ok(JsonFormatter.Formatter(response));
    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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

        return NoContent();
    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateClient(int id,
    [FromServices] HttpClient http,
    [FromBody] RequestPostClient request)
    {
        var httpResponse = await http.PutAsJsonAsync(ServicesURL.Client("update", id), request);

        if (httpResponse.IsSuccessStatusCode)
        {
            var response = new ResponsePostClient { Name = request.Name, Email = request.Email, Age = request.Age };
            return Ok(response);
        }
        return NoContent();
        
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
    [FromServices] HttpClient http,
    [FromHeader] int id)
    {
        var httpResponse = await http.DeleteAsync(ServicesURL.Client("delete", id));

        if (httpResponse.IsSuccessStatusCode)
        {
            return Ok();
        }

        return NoContent();

    }

}
