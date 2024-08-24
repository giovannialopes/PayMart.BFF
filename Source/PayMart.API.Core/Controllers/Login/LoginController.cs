using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Login;
using PayMart.Domain.Core.Response.Login;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("GetUser")]
        [ProducesResponseType(typeof(ResponsePostLogin), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponsePostLogin), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser(
            [FromServices] HttpClient http,
            [FromBody] RequestGetUserLogin request)
        {
            var response = await HttpResponseHandler.PostAsync<ResponsePostLogin>(http, ServicesURL.Login("getUser"), request);
            SaveResponse.SaveUserToken(response.Token);
            return Ok(response);
        }

        [HttpPost]
        [Route("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponsePostLogin), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser(
            [FromServices] HttpClient http,
            [FromBody] RequestRegisterUserLogin request)
        {
            var response = await HttpResponseHandler.PostAsync<ResponsePostLogin>(http, ServicesURL.Login("registerUser"), request);
            return Created("", response);
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
                return Ok();
            return NoContent();
        }

    }
}
