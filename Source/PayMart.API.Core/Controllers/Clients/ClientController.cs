using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayMart.Application.Core.NovaPasta;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Client;
using PayMart.Domain.Core.Response.Login;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Clients;

[Route("api/[Controller]")]
[ApiController]
[Authorize]
public class ClientController : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllClient(
        [FromServices] HttpClient http)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Client("getAll"));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return BadRequest(ResourceExceptionsClient.ERRO_NAO_POSSUI_CLIENTE);
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetIDClient(
        [FromServices] HttpClient http,
        [FromHeader] int id)
        {
        var httpResponse = await http.GetStringAsync(ServicesURL.Client("getID", id));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return BadRequest(ResourceExceptionsClient.ERRO_NAO_POSSUI_CLIENTE);

    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostClient(
        [FromServices] HttpClient http,
        [FromBody] RequestPostClient request)
    {
        string Token = SaveResponse.GetUserToken();
        var response = await HttpResponseHandler.PostAsync<ResponsePostClient>(http, ServicesURL.Client("post", TakeIdJwt.GetUserIdFromToken(Token)), request);
        if (response == null)
            return BadRequest(ResourceExceptionsClient.ERRO_EMAIL_REGISTRADO);

        return Created("", response);
    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateClient(int id,
        [FromServices] HttpClient http,
        [FromBody] RequestPostClient request)
    {
        string token = SaveResponse.GetUserToken();
        var response = await HttpResponseHandler.PutAsync<ResponsePostClient>(http, ServicesURL.Client("update", id, TakeIdJwt.GetUserIdFromToken(token)), request);
        if (response == null)
            return BadRequest(ResourceExceptionsClient.ERRO_EMAIL_REGISTRADO);

        return Ok(response);
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        [FromServices] HttpClient http,
        [FromHeader] int id)
    {
        var deleteLogin = await HttpResponseHandler.DeleteAsync(http, ServicesURL.Login("delete", id));
        var response = await HttpResponseHandler.DeleteAsync(http, ServicesURL.Client("delete", id));

        if (response == null)
            return BadRequest(ResourceExceptionsClient.ERRO_NAO_POSSUI_CLIENTE);

        return Ok();

    }

}
