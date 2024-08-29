using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayMart.Application.Core.NovaPasta;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Order;
using PayMart.Domain.Core.Response.Order;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Orders;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class OrdersController(HttpClient http) : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllOrder()
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Order("getAll"));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return BadRequest(ResourceExceptionsOrder.ERRO_NAO_POSSUI_ORDER);
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetIDOrder(
        [FromHeader] int id)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Order("getID", id));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return BadRequest(ResourceExceptionsOrder.ERRO_NAO_POSSUI_ORDER);
    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostOrder(
        [FromBody] RequestPostOrder request)
    {
        string Token = SaveResponse.GetUserToken();
        var response = await HttpResponseHandler.PostAsync<ResponsePostOrder>(http, ServicesURL.Order("post", TakeIdJwt.GetUserIdFromToken(Token)), request);
        if (response == null)
            return BadRequest(ResourceExceptionsOrder.ERRO_PEDIDO_JA_CRIADO);

        SaveResponse.SaveOrderId(response.id);
        return Created("", ResourceExceptionsOrder.PEDIDO_CRIADO.Replace("#name", $"{response.Name}"));
    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOrder(
        [FromBody] RequestPostOrder request,
        [FromHeader] int id)
    {
        string token = SaveResponse.GetUserToken();
        var response = await HttpResponseHandler.PutAsync<ResponsePostOrder>(http, ServicesURL.Order("update", id, TakeIdJwt.GetUserIdFromToken(token)), request);
        if (response == null)
            return BadRequest(ResourceExceptionsOrder.ERRO_PEDIDO_JA_CRIADO);

        return Ok(response);
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteOrder(
        [FromHeader] int id)
    {
        var response = await HttpResponseHandler.DeleteAsync(http, ServicesURL.Order("delete", id));
        if (response == null)
            return BadRequest(ResourceExceptionsOrder.ERRO_NAO_POSSUI_ORDER);

        return Ok();
    }
}
