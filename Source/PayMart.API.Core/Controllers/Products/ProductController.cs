using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayMart.Application.Core.NovaPasta;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Product;
using PayMart.Domain.Core.Response.Order;
using PayMart.Domain.Core.Response.Product;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Products;

[Route("api/[Controller]")]
[ApiController]
[Authorize]

public class ProductController(HttpClient http) : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()

    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Product("getAll"));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return BadRequest(ResourceExceptionsProducts.PRODUTO_NAO_ENCONTRADO);
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetID(
        [FromHeader] int id)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Product("getID", id));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return BadRequest(ResourceExceptionsProducts.PRODUTO_NAO_ENCONTRADO);

    }

    [HttpGet]
    [Route("RestartProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RestartProduct() 
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Product("restartProduct"));

        return Ok(ResourceExceptionsProducts.CARRINHO_VAZIO);
    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(       
        [FromBody] RequestPostProduct request)
    {
        string Token = SaveResponse.GetUserToken();
        var response = await HttpResponseHandler.PostAsync<ResponsePostProduct>(http, ServicesURL.Product("post", TakeIdJwt.GetUserIdFromToken(Token)), request);
        if (response == null)
            return BadRequest(ResourceExceptionsProducts.PRODUTO_NAO_ENCONTRADO);

        return Created("", response);
    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromBody] RequestPostProduct request,
        [FromHeader] int id)
    {
        string token = SaveResponse.GetUserToken();
        var response = await HttpResponseHandler.PutAsync<ResponsePostProduct>(http, ServicesURL.Product("update", id, TakeIdJwt.GetUserIdFromToken(token)), request);
        if (response == null)
            return BadRequest(ResourceExceptionsProducts.PRODUTO_NAO_ENCONTRADO);

        return Ok(response);
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(
        [FromHeader] int id)
    {
        var response = await HttpResponseHandler.DeleteAsync(http, ServicesURL.Product("delete", id));
        if (response == null)
            return BadRequest(ResourceExceptionsProducts.PRODUTO_NAO_ENCONTRADO);

        return Ok();
    }

}
