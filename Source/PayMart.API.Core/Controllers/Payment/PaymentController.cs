using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayMart.API.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Payment;
using PayMart.Domain.Core.Response.Payment;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Payment;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class PaymentController(HttpClient http) : ControllerBase
{
    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostPayment(
    [FromBody] RequestPostPayment request)
    {
        string price = SaveResponse.GetPrice();
        int productID = SaveResponse.GetOrderId();

        var response = await HttpResponseHandler.PostAsync<ResponsePostPayment>(http, ServicesURL.Payment("post", price, productID), request);
        if (response == null)
            return BadRequest(ResourceExceptionsPayment.ERRO_PAGAMENTO_INVALIDO);

        return Created("", response);
    }
}
