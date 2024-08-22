using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        var httpResponse = await http.PostAsJsonAsync(ServicesURL.Payment("post"), request);

        if (httpResponse.IsSuccessStatusCode)
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponsePostPayment>(responseContent);

            return Created("", response);
        }

        return NoContent();
    }
}
