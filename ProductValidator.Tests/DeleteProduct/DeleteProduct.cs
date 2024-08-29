using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Products;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.Request.Client;
using PayMart.Domain.Core.Response.Product;
using System.Net;

namespace ProductValidator.Tests.DeleteProduct;

public class DeleteProduct
{
    [Fact]
    public async Task DeleteProductSucess()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostProduct { Name = "teste", Description = "o teste é rapido", Price = 12 };
        var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });


        var httpClient = new HttpClient(handlerMock.Object);
        var controller = new ProductController(httpClient);
        int id = 1;

        // Act
        var result = await controller.Delete(id);

        // Assert
        var okResult = result.Should().BeOfType<OkResult>().Subject;
        var actualContent = okResult.StatusCode;
    }

    [Fact]
    public async Task DeleteProductError()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(string.Empty)
            });


        var httpClient = new HttpClient(handlerMock.Object);
        var controller = new ProductController(httpClient);
        int id = 1;

        // Act
        var result = await controller.Delete(id);


        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsProducts.PRODUTO_NAO_ENCONTRADO);
    }
}
