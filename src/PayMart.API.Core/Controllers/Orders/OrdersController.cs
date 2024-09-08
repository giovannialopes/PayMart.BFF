using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayMart.API.Core.Utilities;
using PayMart.Domain.Core.Model;
using PayMart.Infrastructure.Core.Services;
using static PayMart.API.Core.Utilities.JsonFormatter;
using static PayMart.Infrastructure.Core.Services.ServicesURL;

namespace PayMart.API.Core.Controllers.Orders;


[Route("api/[controller]")]
[ApiController]
[Authorize]

public class OrdersController(HttpClient httpClient) : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllOrder()
    {
        var httpResponse = await httpClient.GetStringAsync(ServicesURL.GetOrderUrl(UrlType.GetAll));

        if (httpResponse.Contains("{"))
        {
            var responseJson = FormatterOrder.FormatterGetAll(httpResponse);
            return Ok(responseJson);
        }

        return BadRequest(httpResponse);
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetIDOrder(
        [FromHeader] int id)
    {
        var httpResponse = await httpClient.GetStringAsync(ServicesURL.GetOrderUrl(UrlType.GetID, id));

        if (httpResponse.Contains("{"))
        {
            var responseJson = FormatterOrder.FormatterGetByID(httpResponse);
            return Ok(responseJson);
        }

        return BadRequest(httpResponse);
    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostOrder(
        [FromBody] ModelOrder.OrderRequest request)
    {
        string Token = SaveResponse.GetUserToken();
        var httpResponse = await httpClient.PostAsJsonAsync(ServicesURL.GetOrderUrl(UrlType.Post, TakeIdJwt.GetUserIdFromToken(Token)), request);
        var (response, errorMessage) = await Http.HandleResponse<ModelOrder.OrderResponse>(httpResponse);

        if (response != null)
            return Created("", response);

        return BadRequest(errorMessage);
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteOrder(
        [FromHeader] int id)
    {
        var httpResponse = await httpClient.DeleteAsync(ServicesURL.GetOrderUrl(UrlType.Delete, id));
        var (response, errorMessage) = await Http.HandleResponse<ModelOrder.OrderResponse>(httpResponse);

        if (response != null)
            return Ok(response);

        return BadRequest(errorMessage);
    }
}
