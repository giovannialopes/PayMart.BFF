using Microsoft.AspNetCore.Mvc;
using PayMart.API.Core.Utilities;
using PayMart.Domain.Core.Model;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Login;

[Route("api/[controller]")]
[ApiController]
public class LoginController(HttpClient httpClient) : ControllerBase
{
    [HttpPost]
    [Route("GetUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUser(
        [FromBody] ModelLogin.RequestUserLogin request)
    {
        var httpResponse = await httpClient.PostAsJsonAsync(ServicesURL.Login("getUser"), request);
        var (response, errorMessage) = await Http.HandleResponse<ModelLogin.ResponsePostLogin>(httpResponse);

        if (response != null)
        {
            SaveResponse.SaveUserToken(response.Token);
            return Ok(response);
        }

        return BadRequest(errorMessage);
    }

    [HttpPost]
    [Route("RegisterUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser(
        [FromBody] ModelLogin.RequestUserLogin request)
    {
        var httpResponse = await httpClient.PostAsJsonAsync(ServicesURL.Login("registerUser"), request);
        var (response, errorMessage) = await Http.HandleResponse<ModelLogin.ResponsePostLogin>(httpResponse);

        if (response != null)
            return Created("", response);

        return BadRequest(errorMessage);
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        [FromHeader] int id)
    {
        var httpResponse = await httpClient.DeleteAsync(ServicesURL.Login("delete", id));
        var (response, errorMessage) = await Http.HandleResponse<ModelLogin.ResponsePostLogin>(httpResponse);

        if (response != null)
            return Ok(response);

        return BadRequest(errorMessage);
    }

}
