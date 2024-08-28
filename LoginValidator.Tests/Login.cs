using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Login;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Login;
using PayMart.Domain.Core.Response.Login;
using System.Net;
using System.Text;

namespace LoginValidator.Tests;

public class Login
{
    [Fact]
    public async Task GetLoginSucess()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostLogin { Token = "test_token" };
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
        var controller = new LoginController(httpClient);
        var request = new RequestGetUserLogin
        {
            Email = "test@example.com",
            Password = "securepassword"
        };

        // Act
        var result = await controller.GetUser(request);

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetLoginError()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();

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
        var controller = new LoginController(httpClient);

        var request = new RequestGetUserLogin
        {
            Email = "test@example.com",
            Password = "securepassword"
        };

        // Act
        var result = await controller.GetUser(request);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().Be(ResourceExceptionsLogin.ERRO_USUARIO_NAO_ENCONTRADO);
    }

    [Fact]
    public async Task RegisterLoginSucess()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostLogin { Token = "test_token" };
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
        var controller = new LoginController(httpClient);
        var request = new RequestRegisterUserLogin
        {
            Name = "test",
            Email = "test@example.com",
            Password = "securepassword"
        };

        // Act
        var result = await controller.RegisterUser(request);

        // Assert
        result.Should().BeOfType<CreatedResult>()
            .Which.Value.Should().BeEquivalentTo(ResourceExceptionsLogin.CRIACAO_DE_USUARIO);
    }

    [Fact]
    public async Task RegisterLoginError()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostLogin { Token = "test_token" };
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
        var controller = new LoginController(httpClient);
        var request = new RequestRegisterUserLogin
        {
            Name = "test",
            Email = "test@example.com",
            Password = "securepassword"
        };

        // Act
        var result = await controller.RegisterUser(request);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().Be(ResourceExceptionsLogin.ERRO_EMAIL_EXISTENTE);
    }
}
