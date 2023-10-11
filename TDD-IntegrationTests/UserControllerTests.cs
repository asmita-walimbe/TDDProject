using System.Text;
using System.Text.Json;
using TDD_IntegrationTests.Constants;

namespace TDD_IntegrationTests;

public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private HttpClient _client;

    public UserControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUserByIdApi_With_Correct_Request_Returns_Success_Response()
    {
        var requestUri = TDDProject.Constants.GetById;
        requestUri = requestUri.Replace("{0}", "1");
        var response = await _client.GetAsync(requestUri);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("0")]
    public async Task GetUserById_With_Incorrect_Request_Returns_Failure(string requestParam)
    {
        var requestUri = TDDProject.Constants.GetById;
        requestUri = string.Format(requestUri, requestParam);
        var response = await _client.GetAsync(requestUri);
        if (requestParam == "0")
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        else
        {
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }

    [Fact]
    public async Task AddUserApi_With_Correct_Request_Returns_Success_Response()
    {
        var requestUri = TDDProject.Constants.UserApi;
        var bodyJson = JsonSerializer.Serialize(TestConstants.user);
        var response = await _client.PostAsync(requestUri, new StringContent(bodyJson, Encoding.UTF8, "application/json"));
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}