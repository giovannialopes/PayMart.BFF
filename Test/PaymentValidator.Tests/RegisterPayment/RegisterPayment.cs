using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Payment;
using PayMart.API.Core.Controllers.Products;
using PayMart.API.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.Request.Payment;
using PayMart.Domain.Core.Request.Product;
using PayMart.Domain.Core.Response.Payment;
using PayMart.Domain.Core.Response.Product;
using System.Net;

namespace PaymentValidator.Tests.RegisterPayment;

public class RegisterPayment
{
    [Fact]
    public async Task RegisterProductSucess()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostPayment { PaymentType = "4", DateTime = DateTime.UtcNow, Price = 12 };
        var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });


        var httpClient = new HttpClient(handlerMock.Object);
        var controller = new PaymentController(httpClient);
        var request = new RequestPostPayment
        {
            PaymentType = 4
        };
        SaveResponse.SaveUserToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.gmsqCPG8I7VH7txA7OxcrsIedHUJrQCLqq9HYz3TlLk");
        SaveResponse.SaveOrderId(1);

        // Act
        var result = await controller.PostPayment(request);

        // Assert
        var createdResult = result.Should().BeOfType<CreatedResult>().Subject;
        var actualContent = createdResult.Value as ResponsePostPayment;
        actualContent.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task RegisterClientError()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostPayment { PaymentType = "4", DateTime = DateTime.UtcNow, Price = 12 };
        var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(string.Empty)
            });


        var httpClient = new HttpClient(handlerMock.Object);
        var controller = new PaymentController(httpClient);
        var request = new RequestPostPayment
        {
            PaymentType = 4
        };
        SaveResponse.SaveUserToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.gmsqCPG8I7VH7txA7OxcrsIedHUJrQCLqq9HYz3TlLk");
        SaveResponse.SaveOrderId(1);

        // Act
        var result = await controller.PostPayment(request);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsPayment.ERRO_PAGAMENTO_INVALIDO);
    }
}
