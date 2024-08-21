using Microsoft.AspNetCore.Mvc;
using PayMart.Application.Core.NovaPasta;
using PayMart.Domain.Core.Request.Login;
using PayMart.Domain.Core.Request.Order;
using PayMart.Domain.Core.Response.Login;
using PayMart.Domain.Core.Response.Order;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("GetUser")]
        [ProducesResponseType(typeof(ResponsePostLogin), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetID(
            [FromServices] HttpClient http,
            [FromBody] RequestPostLogin request)
        {
            var httpResponse = await http.PostAsJsonAsync(ServicesURL.Login("getID"), request);

            if (httpResponse.IsSuccessStatusCode)
            {
                var response = new ResponsePostLogin { };

                return Ok(response);
            }

            return NoContent();
        }


        [HttpPost]
        [Route("RegisterUser")]
        [ProducesResponseType(typeof(ResponsePostLogin), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Register(
            [FromServices] HttpClient http,
            [FromBody] RequestPostLogin request)
        {
            var httpResponse = await http.PostAsJsonAsync(ServicesURL.Login("post"), request);

            if (httpResponse.IsSuccessStatusCode)
            {
                var response = new ResponsePostLogin { };

                return Ok(response);
            }

            return NoContent();
        }

    }
}
