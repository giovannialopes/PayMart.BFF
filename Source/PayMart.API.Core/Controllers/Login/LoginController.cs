using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser(
            [FromServices] HttpClient http,
            [FromBody] RequestGetUserLogin request)
        {
            var response = await HttpResponseHandler.PostAsync<ResponsePostLogin>(http, ServicesURL.Login("getUser"), request);
            if (response == null)
                return BadRequest(ResourceExceptionsLogin.ERRO_USUARIO_NAO_ENCONTRADO);

            SaveResponse.SaveUserToken(response.Token);
            return Ok(response);
        }

        [HttpPost]
        [Route("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser(
            [FromServices] HttpClient http,
            [FromBody] RequestRegisterUserLogin request)
        {
            var response = await HttpResponseHandler.PostAsync<ResponsePostLogin>(http, ServicesURL.Login("registerUser"), request);
            if (response == null)
                return BadRequest(ResourceExceptionsLogin.ERRO_EMAIL_EXISTENTE);

            return Created("", ResourceExceptionsLogin.CRIACAO_DE_USUARIO);
        }

        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(
            [FromServices] HttpClient http,
            [FromHeader] int id)
        {
            var response = await HttpResponseHandler.DeleteAsync(http, ServicesURL.Login("delete", id));
            if (response == null)
                return BadRequest();

            return Ok();
        }

    }
}
