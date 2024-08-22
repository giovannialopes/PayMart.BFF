using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayMart.Application.Core.NovaPasta;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Client;
using PayMart.Infrastructure.Core.Services;
using System.Text;

namespace PayMart.API.Core.Controllers.Clients;

[Route("api/[Controller]")]
[ApiController]
public class ClientController : ControllerBase
{

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllClient(
        [FromServices] HttpClient http)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Client("getAll"));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return BadRequest();
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetIDClient(
        [FromServices] HttpClient http,
        [FromHeader] int id)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Client("getID", id));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return BadRequest();

    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PostClient(
        [FromServices] HttpClient http,
        [FromBody] RequestPostClient request)
    {
        int userID = SaveResponse.GetUserId();
        var httpResponse = await http.PostAsJsonAsync(ServicesURL.Client("post", userID), request);

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
        int userID = SaveResponse.GetUserId();
        var httpResponse = await http.PutAsJsonAsync(ServicesURL.Client("update", id, userID), request);

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
            var httpResponseDelete = await http.DeleteAsync(ServicesURL.Login("delete", id));

            return Ok();
        }

        return NoContent();

    }

}
