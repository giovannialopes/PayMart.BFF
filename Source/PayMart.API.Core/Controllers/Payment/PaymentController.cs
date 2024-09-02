using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayMart.API.Core.Utilities;
using PayMart.Domain.Core.Model;
using PayMart.Infrastructure.Core.Services;
using static PayMart.API.Core.Utilities.JsonFormatter;

namespace PayMart.API.Core.Controllers.Payment;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class PaymentController(HttpClient httpClient) : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPayment()
    {
        var httpResponse = await httpClient.GetStringAsync(ServicesURL.Payment("getAll"));

        if (httpResponse.Contains("{"))
        {
            var responseJson = FormatterPayment.FormatterGetAll(httpResponse);
            return Ok(responseJson);
        }

        return BadRequest(httpResponse);
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaymentById(
        [FromHeader] int orderId)
    {
        var httpResponse = await httpClient.GetStringAsync(ServicesURL.Payment("getID", orderId));

        if (httpResponse.Contains("{"))
        {
            var responseJson = FormatterPayment.FormatterGetByID(httpResponse);
            return Ok(responseJson);
        }

        return BadRequest(httpResponse);
    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostPayment(
        [FromBody] ModelPayment.PaymentRequest request,
        [FromHeader] int orderId)
    {
        string Token = SaveResponse.GetUserToken();

        var httpResponse = await httpClient.PostAsJsonAsync(ServicesURL.Payment("post", orderId, TakeIdJwt.GetUserIdFromToken(Token)), request);
        var (response, errorMessage) = await Http.HandleResponse<ModelPayment.PaymentResponse>(httpResponse);

        if (response != null)
            return Created("", response);

        return BadRequest(errorMessage);
    }

}
