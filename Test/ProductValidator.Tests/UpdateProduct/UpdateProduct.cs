using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Clients;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Client;
using System.Net;
using PayMart.API.Core.Controllers.Products;
using PayMart.Domain.Core.Request.Product;
using PayMart.Domain.Core.Response.Product;

namespace ProductValidator.Tests.UpdateProduct;

public class UpdateProduct
{
    [Fact]
    public async Task UpdateProductSucess()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostProduct { Name = "teste", Description = "o teste é rapido", Price = 12 };
        var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });


        var httpClient = new HttpClient(handlerMock.Object);
        var controller = new ProductController(httpClient);
        var request = new RequestPostProduct
        {
            Name = "teste",
            Description = "o teste é rapido",
            Amount = 17,
            Price = 12,
        };
        SaveResponse.SaveUserToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.gmsqCPG8I7VH7txA7OxcrsIedHUJrQCLqq9HYz3TlLk");
        int id = 1;

        // Act
        var result = await controller.UpdateProduct(request,id);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var actualContent = okResult.Value as ResponsePostProduct;
        actualContent.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task UpdateProductError()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostProduct { Name = "teste", Description = "o teste é rapido", Price = 12 };
        var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(string.Empty)
            });


        var httpClient = new HttpClient(handlerMock.Object);
        var controller = new ProductController(httpClient);
        var request = new RequestPostProduct
        {
            Name = "teste",
            Description = "o teste é rapido",
            Amount = 17,
            Price = 12,
        };
        SaveResponse.SaveUserToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.gmsqCPG8I7VH7txA7OxcrsIedHUJrQCLqq9HYz3TlLk");
        int id = 1;

        // Act
        var result = await controller.UpdateProduct(request,1);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsProducts.PRODUTO_NAO_ENCONTRADO);
    }

}
