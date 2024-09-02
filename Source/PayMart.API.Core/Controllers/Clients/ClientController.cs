using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayMart.API.Core.Utilities;
using PayMart.Domain.Core.Model;
using PayMart.Infrastructure.Core.Services;
using static PayMart.API.Core.Utilities.JsonFormatter;

namespace PayMart.API.Core.Controllers.Clients;

[Route("api/[controller]")]
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

        if (httpResponse.Contains("{"))
        {
            var responseJson = FormatterClient.FormatterGetAll(httpResponse);
            return Ok(responseJson);
        }

        return BadRequest(httpResponse);
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetIDClient(
        [FromHeader] int id)
    {
        var httpResponse = await httpClient.GetStringAsync(ServicesURL.Client("getID", id));

        if (httpResponse.Contains("{"))
        {
            var responseJson = FormatterClient.FormatterGetByID(httpResponse);
            return Ok(responseJson);
        }

        return BadRequest(httpResponse);

    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateClient(int id,
        [FromBody] ModelClient.ClientRequest request)
    {
        string Token = SaveResponse.GetUserToken();
        var httpResponse = await httpClient.PutAsJsonAsync(ServicesURL.Client("update", id , TakeIdJwt.GetUserIdFromToken(Token)), request);
        var (response, errorMessage) = await Http.HandleResponse<ModelClient.ClientResponse>(httpResponse);

        if (response != null)
            return Ok(response);

        return BadRequest(errorMessage);
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        [FromHeader] int id)
    {
        var httpResponse = await httpClient.DeleteAsync(ServicesURL.Client("delete", id));
        var (response, errorMessage) = await Http.HandleResponse<ModelClient.ClientResponse>(httpResponse);

        if (response != null)
            return Ok(response);

        return BadRequest(errorMessage);

    }

}
