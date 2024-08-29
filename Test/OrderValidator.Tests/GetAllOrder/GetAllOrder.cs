using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Orders;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.Response.Order;
using System.Net;

namespace OrderValidator.Tests.GetAllOrder;

public class GetAllOrder
{
    [Fact]
    public async Task GetAllOrderSucess()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new List<ResponsePostOrder>
    {
        new ResponsePostOrder { Name = "teste", Date = DateTime.UtcNow }
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
        var controller = new OrdersController(httpClient);

        // Act
        var result = await controller.GetAllOrder();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var actualContent = okResult.Value as string;
        var actualResponse = JsonConvert.DeserializeObject<List<ResponsePostOrder>>(actualContent!);

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }


    [Fact]
    public async Task GetAllOrderError()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new List<ResponsePostOrder>
    {
        new ResponsePostOrder { Name = "teste", Date = DateTime.UtcNow }
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
        var controller = new OrdersController(httpClient);

        // Act
        var result = await controller.GetAllOrder();

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsOrder.ERRO_NAO_POSSUI_ORDER);
    }
}
