using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Login;
using PayMart.Domain.Core.Response.Login;
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
        [ProducesResponseType(typeof(ResponsePostLogin), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser(
            [FromServices] HttpClient http,
            [FromBody] RequestGetUserLogin request)
        {
            var httpResponse = await http.PostAsJsonAsync(ServicesURL.Login("getUser"), request);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponsePostLogin>(responseContent);

                SaveResponse.SaveUserId(response!.Id);
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponsePostLogin), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser(
            [FromServices] HttpClient http,
            [FromBody] RequestRegisterUserLogin request)
        {
            var httpResponse = await http.PostAsJsonAsync(ServicesURL.Login("registerUser"), request);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponsePostLogin>(responseContent);
         
                var requestClient = JsonConvert.DeserializeObject<ResponsePostClient>(responseContent);
                var httpResponseClient = await http.PostAsJsonAsync(ServicesURL.Client("post", response!.Id), requestClient);

                SaveResponse.SaveUserId(response.Id);
                return Created("","Usuário criado com Sucesso!");
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(typeof(ResponsePostClient), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(
            [FromServices] HttpClient http,
            [FromHeader] int id)
        {
            var httpResponse = await http.DeleteAsync(ServicesURL.Login("delete", id));

            if (httpResponse.IsSuccessStatusCode)
            {
                return Ok();
            }

            return NoContent();

        }

    }
}
