using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayMart.API.Core.Utilities;
using PayMart.Domain.Core.Model;
using PayMart.Infrastructure.Core.Services;
using static PayMart.API.Core.Utilities.JsonFormatter;

namespace PayMart.API.Core.Controllers.Products;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController(HttpClient httpClient) : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllProduct()
    {
        var httpResponse = await httpClient.GetStringAsync(ServicesURL.Product("getAll"));

        if (httpResponse.Contains("{"))
        {
            var responseJson = FormatterProduct.FormatterGetAll(httpResponse);
            return Ok(responseJson);
        }

        return BadRequest(httpResponse);
    }

    [HttpGet]
    [Route("GetID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetIDProduct(
        [FromHeader] int id)
    {
        var httpResponse = await httpClient.GetStringAsync(ServicesURL.Product("getID", id));

        if (httpResponse.Contains("{"))
        {
            var responseJson = FormatterProduct.FormatterGetByID(httpResponse);
            return Ok(responseJson);
        }

        return BadRequest(httpResponse);

    }

    [HttpPost]
    [Route("Post")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostProduct(
        [FromBody] ModelProduct.ProductRequest request)
    {
        string Token = SaveResponse.GetUserToken();
        var httpResponse = await httpClient.PostAsJsonAsync(ServicesURL.Product("post", TakeIdJwt.GetUserIdFromToken(Token)), request);
        var (response, errorMessage) = await Http.HandleResponse<ModelProduct.ProductResponse>(httpResponse);

        if (response != null)
            return Created("", response);

        return BadRequest(errorMessage);
    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(
        [FromBody] ModelProduct.ProductRequest request,
        [FromHeader] int id)
    {
        string Token = SaveResponse.GetUserToken();
        var httpResponse = await httpClient.PutAsJsonAsync(ServicesURL.Product("update", id, TakeIdJwt.GetUserIdFromToken(Token)), request);
        var (response, errorMessage) = await Http.HandleResponse<ModelProduct.ProductResponse>(httpResponse);

        if (response != null)
            return Ok(response);

        return BadRequest(errorMessage);
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProduct(
        [FromHeader] int id)
    {
        var httpResponse = await httpClient.DeleteAsync(ServicesURL.Product("delete", id));
        var (response, errorMessage) = await Http.HandleResponse<ModelProduct.ProductResponse>(httpResponse);

        if (response != null)
            return Ok(response);

        return BadRequest(errorMessage);
    }

}
