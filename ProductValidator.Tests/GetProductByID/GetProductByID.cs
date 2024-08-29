using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Clients;
using PayMart.API.Core.Controllers.Products;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Response.Product;
using System.Net;

namespace ProductValidator.Tests.GetProductByID;

public class GetProductByID
{
    [Fact]
    public async Task GetProductByIDSucess()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostProduct { Name = "teste", Description = "o teste é rapido", Price = 12 };

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
        int id = 1;

        // Act
        var result = await controller.GetID(id);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var actualContent = okResult.Value as string;
        var actualResponse = JsonConvert.DeserializeObject<ResponsePostProduct>(actualContent!);

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetProductByIDError()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostProduct { Name = "teste", Description = "o teste é rapido", Price = 12 };

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
        int id = 1;

        // Act
        var result = await controller.GetID(id);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsProducts.PRODUTO_NAO_ENCONTRADO);


    }
}
