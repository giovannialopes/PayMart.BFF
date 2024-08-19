using Microsoft.AspNetCore.Mvc;

namespace PayMart.API.Core.Controllers.Products;

[Route("api/[Controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpGet]
    [Route("GetID")]
    public IActionResult GetID()
    {
        return Ok();
    }

    [HttpPost]
    [Route("Post")]
    public IActionResult Post()
    {
        return Ok();
    }

    [HttpPut]
    [Route("Update")]
    public IActionResult Update()
    {
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public IActionResult Delete()
    {
        return Ok();
    }

}
