using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Payment;
using PayMart.Domain.Core.Response.Payment;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Payment;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class PaymentController : ControllerBase
{
    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(
    [FromServices] HttpClient http,
    [FromBody] RequestPostPayment request)
    {
        string price = SaveResponse.GetPrice();
        int productID = SaveResponse.GetOrderId();

        var response = await HttpResponseHandler.PostAsync<ResponsePostClient>(http, ServicesURL.Payment("post", price, productID), request);
        if (response == null)
            return BadRequest(ResourceExceptionsPayment.ERRO_PAGAMENTO_INVALIDO);

        return Created("", response);
    }
}
