using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Clients;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using System.Net;
using PayMart.Domain.Core.Response.Product;
using PayMart.API.Core.Controllers.Products;

namespace ProductValidator.Tests.GetAllProduct;

public class GetAllProduct
{
    [Fact]
    public async Task GetAllProductSucess()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new List<ResponsePostProduct>
    {
        new ResponsePostProduct { Name = "teste", Description = "o teste é rapido", Price = 12 }
    };

        var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var controller = new ProductController(httpClient);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var actualContent = okResult.Value as string;
        var actualResponse = JsonConvert.DeserializeObject<List<ResponsePostProduct>>(actualContent!);

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetAllProductError()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new List<ResponsePostProduct>
    {
        new ResponsePostProduct { Name = "teste", Description = "o teste é rapido", Price = 12 }
    };

        var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(string.Empty)
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var controller = new ProductController(httpClient);

        // Act
        var result = await controller.GetAll();

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsProducts.PRODUTO_NAO_ENCONTRADO);
    }
}
