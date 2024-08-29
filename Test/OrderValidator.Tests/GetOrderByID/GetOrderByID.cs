using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Clients;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using System.Net;
using PayMart.Domain.Core.Response.Order;
using PayMart.API.Core.Controllers.Orders;

namespace OrderValidator.Tests.GetOrderByID;

public class GetOrderByID
{
    [Fact]
    public async Task GetOrderByIDSucess()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostOrder { Name = "teste", Date = DateTime.UtcNow };
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
        int id = 1;

        // Act
        var result = await controller.GetIDOrder(id);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var actualContent = okResult.Value as string;
        var actualResponse = JsonConvert.DeserializeObject<ResponsePostOrder>(actualContent!);

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetOrderByIDError()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostOrder { Name = "teste", Date = DateTime.UtcNow };


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
        int id = 1;

        // Act
        var result = await controller.GetIDOrder(id);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsOrder.ERRO_NAO_POSSUI_ORDER);


    }
}
