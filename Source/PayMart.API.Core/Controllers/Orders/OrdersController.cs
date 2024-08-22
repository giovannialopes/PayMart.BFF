using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayMart.Application.Core.NovaPasta;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.Request.Order;
using PayMart.Domain.Core.Response.Login;
using PayMart.Domain.Core.Response.Order;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.API.Core.Controllers.Orders;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(ResponsePostOrder), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll(
       [FromServices] HttpClient http)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Order("getAll"));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return BadRequest();
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(typeof(ResponsePostOrder), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetID(
        [FromServices] HttpClient http,
        [FromHeader] int id)
    {
        var httpResponse = await http.GetStringAsync(ServicesURL.Order("getID", id));

        if (string.IsNullOrEmpty(httpResponse) == false)
        {
            return Ok(JsonFormatter.Formatter(httpResponse));
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(typeof(ResponsePostOrder), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Post(
        [FromServices] HttpClient http,
        [FromBody] RequestPostOrder request)
    {
        int userID = SaveResponse.GetUserId();
        var httpResponse = await http.PostAsJsonAsync(ServicesURL.Order("post", userID), request);

        if (httpResponse.IsSuccessStatusCode)
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponsePostOrder>(responseContent);

            var httpSum = await http.GetStringAsync(ServicesURL.Product("getSumProducts", response!.ProductID));

            SaveResponse.SavePrice(httpSum);
            SaveResponse.SaveOrderId(response.id);

            return Created("", response);
        }

        return NoContent();
    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(typeof(ResponsePostOrder), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(
        [FromServices] HttpClient http,
        [FromBody] RequestPostOrder request,
        [FromHeader] int id)
    {
        var httpResponse = await http.PutAsJsonAsync(ServicesURL.Order("update", id), request);

        if (httpResponse.IsSuccessStatusCode)
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponsePostOrder>(responseContent);

            return Ok(response);
        }

        return NoContent();
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(typeof(ResponsePostOrder), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        [FromServices] HttpClient http,
        [FromHeader] int id)
    {
        var httpResponse = await http.DeleteAsync(ServicesURL.Order("delete", id));

        if (httpResponse.IsSuccessStatusCode)
        {
            return Ok();
        }

        return NoContent();
    }
}
