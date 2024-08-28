using Microsoft.AspNetCore.Mvc;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Login;
using PayMart.Domain.Core.Response.Login;
using PayMart.Infrastructure.Core.Services;
using System.Net.Http;
using System;
using Newtonsoft.Json;

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
        [FromBody] RequestGetUserLogin request)
    {
        var response = await HttpResponseHandler.PostAsync<ResponsePostLogin>(httpClient, ServicesURL.Login("getUser"), request);

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
        [FromBody] RequestRegisterUserLogin request)
    {
        var response = await HttpResponseHandler.PostAsync<ResponsePostLogin>(httpClient, ServicesURL.Login("registerUser"), request);
        if (response == null)
            return BadRequest(ResourceExceptionsLogin.ERRO_EMAIL_EXISTENTE);

        return Created("", ResourceExceptionsLogin.CRIACAO_DE_USUARIO);
    }
}
