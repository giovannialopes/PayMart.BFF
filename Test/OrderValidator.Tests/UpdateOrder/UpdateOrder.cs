using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Orders;
using PayMart.API.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Order;
using PayMart.Domain.Core.Response.Order;
using System.Net;

namespace OrderValidator.Tests.UpdateOrder;

public class UpdateOrder
{
    [Fact]
    public async Task UpdateOrderSucess()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostOrder { Name = "teste", Date = DateTime.UtcNow };
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
        var controller = new OrdersController(httpClient);
        var request = new RequestPostOrder
        {
            ProductID = 1,
            Name = "test",
        };
        SaveResponse.SaveUserToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.gmsqCPG8I7VH7txA7OxcrsIedHUJrQCLqq9HYz3TlLk");
        int id = 1;

        // Act
        var result = await controller.UpdateOrder(request, id);

        // Assert
        var uptadeResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var actualContent = uptadeResult.Value as ResponsePostOrder;
        actualContent.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task UpdateOrderError()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostClient { Name = "teste", Age = 0, Email = "teste@gmail.com" };
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
        var controller = new OrdersController(httpClient);
        var request = new RequestPostOrder
        {
            ProductID = 1,
            Name = "test",
        };
        SaveResponse.SaveUserToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.gmsqCPG8I7VH7txA7OxcrsIedHUJrQCLqq9HYz3TlLk");
        int id = 1;

        // Act
        var result = await controller.UpdateOrder(request, id);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsOrder.ERRO_PEDIDO_JA_CRIADO);
    }
}
