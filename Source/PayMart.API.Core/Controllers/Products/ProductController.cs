﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayMart.Application.Core.NovaPasta;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Product;
using PayMart.Domain.Core.Response.Order;
using PayMart.Domain.Core.Response.Product;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Products;

[Route("api/[Controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(ResponsePostProduct), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll(
        [FromServices] HttpClient http)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Product("getAll"));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return NoContent();
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(typeof(ResponsePostProduct), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetID(
        [FromServices] HttpClient http,
        [FromHeader] int id)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Product("getID", id));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return NoContent();
    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(typeof(ResponsePostProduct), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Post(
        [FromServices] HttpClient http,
        [FromBody] RequestPostProduct request)
    {
        string Token = SaveResponse.GetUserToken();
        string userID = TakeIdJwt.GetUserIdFromToken(Token);

        var httpResponse = await http.PostAsJsonAsync(ServicesURL.Product("post", 1), request);

        if (httpResponse.IsSuccessStatusCode)
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponsePostOrder>(responseContent);

            return Created("", response);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("RestartProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RestartProduct(
    [FromServices] HttpClient http)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Product("postRestart"));

        return Ok("O carrinho não possui mais produtos!");
    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(typeof(ResponsePostProduct), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(
        [FromServices] HttpClient http,
        [FromBody] RequestPostProduct request,
        [FromHeader] int id)
    {
        string Token = SaveResponse.GetUserToken();
        string userID = TakeIdJwt.GetUserIdFromToken(Token);

        var httpResponse = await http.PutAsJsonAsync(ServicesURL.Product("update", id, userID), request);

        if (httpResponse.IsSuccessStatusCode)
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponsePostOrder>(responseContent);

            return Ok(response);
        }

        return NoContent();
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(typeof(ResponsePostProduct), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        [FromServices] HttpClient http,
        [FromHeader] int id)
    {
        var httpResponse = await http.DeleteAsync(ServicesURL.Product("delete", id));

        if (httpResponse.IsSuccessStatusCode)
        {
            return Ok();
        }

        return NoContent();
    }

}
