using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser(
            [FromServices] HttpClient http,
            [FromBody] RequestPostLogin request)
        {
            var httpResponse = await http.PostAsJsonAsync(ServicesURL.Login("getUser"), request);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponsePostLogin>(responseContent);
                return Ok(response);
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("RegisterUser")]
        [ProducesResponseType(typeof(ResponsePostLogin), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser(
            [FromServices] HttpClient http,
            [FromBody] RequestPostLogin request)
        {
            var httpResponse = await http.PostAsJsonAsync(ServicesURL.Login("registerUser"), request);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponsePostLogin>(responseContent);

                return Ok(response);
            }

            return BadRequest();
        }

    }
}
