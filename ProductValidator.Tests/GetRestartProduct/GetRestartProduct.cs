using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Products;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.Response.Product;
using System.Net;

namespace ProductValidator.Tests.GetRestartProduct;

public class GetRestartProduct
{
    [Fact]
    public async Task GetRestartProductSucess()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var controller = new ProductController(httpClient);

        // Act
        var result = await controller.RestartProduct();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var actualContent = okResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsProducts.CARRINHO_VAZIO);
    }
}
