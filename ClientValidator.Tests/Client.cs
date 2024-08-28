using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PayMart.API.Core.Controllers.Clients;
using PayMart.Application.Core.Utilities;
using PayMart.Domain.Core.Exception.ResourceExceptions;
using PayMart.Domain.Core.NovaPasta.NovaPasta;
using PayMart.Domain.Core.Request.Client;
using System.Net;

namespace ClientValidator.Tests;

public class Client
{
    [Fact]
    public async Task GetAllClientSucess()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new List<ResponsePostClient>
    {
        new ResponsePostClient { Name = "teste", Age = 0, Email = "teste@gmail.com" }
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
        var controller = new ClientController(httpClient);

        // Act
        var result = await controller.GetAllClient();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var actualContent = okResult.Value as string;
        var actualResponse = JsonConvert.DeserializeObject<List<ResponsePostClient>>(actualContent!);

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetAllClientError()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new List<ResponsePostClient>
    {
        new ResponsePostClient { Name = "teste", Age = 0, Email = "teste@gmail.com" }
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
        var controller = new ClientController(httpClient);

        // Act
        var result = await controller.GetAllClient();

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsClient.ERRO_NAO_POSSUI_CLIENTE);
    }

    [Fact]
    public async Task GetClientByIDSucess()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostClient
        { Name = "teste", Age = 0, Email = "teste@gmail.com" };

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
        var controller = new ClientController(httpClient);

        // Act
        var result = await controller.GetAllClient();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var actualContent = okResult.Value as string;
        var actualResponse = JsonConvert.DeserializeObject<ResponsePostClient>(actualContent!);

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetClientByIDError()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostClient
        { Name = "teste", Age = 0, Email = "teste@gmail.com" };

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
        var controller = new ClientController(httpClient);

        // Act
        var result = await controller.GetAllClient();

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsClient.ERRO_NAO_POSSUI_CLIENTE);


    }

    [Fact]
    public async Task RegisterClientSucess()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostClient { Name = "teste", Age = 0, Email = "teste@gmail.com" };
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
        var controller = new ClientController(httpClient);
        var request = new RequestPostClient
        {
            Name = "test",
            Email = "test@example.com",
            Age = 0,
            Address = "002",
            PhoneNumber = "1234567890",
            UserID = 1
            
        };

        // Act
        SaveResponse.SaveUserToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.gmsqCPG8I7VH7txA7OxcrsIedHUJrQCLqq9HYz3TlLk");
        var result = await controller.PostClient(request);

        // Assert
        var createdResult = result.Should().BeOfType<CreatedResult>().Subject;
        var actualContent = createdResult.Value as ResponsePostClient;
        actualContent.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task RegisterClientError()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostClient { Name = "teste", Age = 0, Email = "teste@gmail.com" };
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
        var controller = new ClientController(httpClient);
        var request = new RequestPostClient
        {
            Name = "test",
            Email = "test@example.com",
            Age = 0,
            Address = "002",
            PhoneNumber = "1234567890",
            UserID = 1

        };

        // Act
        SaveResponse.SaveUserToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.gmsqCPG8I7VH7txA7OxcrsIedHUJrQCLqq9HYz3TlLk");
        var result = await controller.PostClient(request);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsClient.ERRO_EMAIL_REGISTRADO);
    }

    [Fact]
    public async Task DeleteClientSucess()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var expectedResponse = new ResponsePostClient { Name = "teste", Age = 0, Email = "teste@gmail.com" };
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
        var controller = new ClientController(httpClient);
        var request = new RequestPostClient
        {
            Name = "test",
            Email = "test@example.com",
            Age = 0,
            Address = "002",
            PhoneNumber = "1234567890",
            UserID = 1

        };
        int id = 1;

        // Act
        var result = await controller.Delete(id);

        // Assert
        var okResult = result.Should().BeOfType<OkResult>().Subject;
        var actualContent = okResult.StatusCode;                
    }

    [Fact]
    public async Task DeleteClientError()
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
        var controller = new ClientController(httpClient);
        var request = new RequestPostClient
        {
            Name = "test",
            Email = "test@example.com",
            Age = 0,
            Address = "002",
            PhoneNumber = "1234567890",
            UserID = 1

        };
        int id = 1;

        // Act
        var result = await controller.Delete(id);


        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var actualContent = badRequestResult.Value as string;
        actualContent.Should().BeEquivalentTo(ResourceExceptionsClient.ERRO_NAO_POSSUI_CLIENTE);
    }





}

