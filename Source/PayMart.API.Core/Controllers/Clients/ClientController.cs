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
public class ClientController(HttpClient httpClient) : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllClient()
    {
        var httpResponse = await httpClient.GetStringAsync(ServicesURL.Client("getAll"));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            var a = JsonFormatter.Formatter(httpResponse);
            return Ok(a);
        }

        return BadRequest(ResourceExceptionsClient.ERRO_NAO_POSSUI_CLIENTE);
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetIDClient(
        [FromHeader] int id)
        {
        var httpResponse = await httpClient.GetStringAsync(ServicesURL.Client("getID", id));

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
        [FromBody] RequestPostClient request)
    {
        string Token = SaveResponse.GetUserToken();
        var response = await HttpResponseHandler.PostAsync<ResponsePostClient>(httpClient, ServicesURL.Client("post", TakeIdJwt.GetUserIdFromToken(Token)), request);
        if (response == null)
            return BadRequest(ResourceExceptionsClient.ERRO_EMAIL_REGISTRADO);

        return Created("", response);
    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateClient(int id,
        [FromBody] RequestPostClient request)
    {
        string token = SaveResponse.GetUserToken();
        var response = await HttpResponseHandler.PutAsync<ResponsePostClient>(httpClient, ServicesURL.Client("update", id, TakeIdJwt.GetUserIdFromToken(token)), request);
        if (response == null)
            return BadRequest(ResourceExceptionsClient.ERRO_EMAIL_REGISTRADO);

        return Ok(response);
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        [FromHeader] int id)
    {
        var response = await HttpResponseHandler.DeleteAsync(httpClient, ServicesURL.Login("delete", id));
        if (response == null)
            return BadRequest(ResourceExceptionsClient.ERRO_NAO_POSSUI_CLIENTE);

        return Ok();

    }

}
