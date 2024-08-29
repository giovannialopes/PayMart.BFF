using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Orders;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.Response.Order;
using System.Net;

namespace OrderValidator.Tests.DeleteOrder;

public class DeleteOrder
{
    [Fact]
    public async Task DeleteOrderSucess()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostOrder { Name = "teste", Date = DateTime.UtcNow };
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
        var controller = new OrdersController(httpClient);
        int id = 1;

        // Act
        var result = await controller.DeleteOrder(id);

        // Assert
        var okResult = result.Should().BeOfType<OkResult>().Subject;
        var actualContent = okResult.StatusCode;
    }

    [Fact]
    public async Task DeleteOrderError()
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
        var controller = new OrdersController(httpClient);
        int id = 1;

        // Act
        var result = await controller.DeleteOrder(id);


        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsOrder.ERRO_NAO_POSSUI_ORDER);
    }
}
