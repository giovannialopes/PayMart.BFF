using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.Request.Payment;
using PayMart.Domain.Core.Response.Payment;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Payment;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(typeof(ResponsePostPayment), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Post(
    [FromServices] HttpClient http,
    [FromBody] RequestPostPayment request)
    {
        string price = SaveResponse.GetPrice();
        int productID = SaveResponse.GetOrderId();

        var httpResponse = await http.PostAsJsonAsync(ServicesURL.Payment("post", price, productID), request);

        if (httpResponse.IsSuccessStatusCode)
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponsePostPayment>(responseContent);

            return Created("", response);
        }

        return NoContent();
    }
}
