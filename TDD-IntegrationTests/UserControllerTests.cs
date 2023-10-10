namespace TDD_IntegrationTests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        public UserControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetUserByIdApi_With_Correct_Request_Returns_Success_Response()
        {
            var requestUri = Constants.GetById;
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
            var requestUri = Constants.GetById;
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
    }
}
