namespace TDD_IntegrationTests
{
    public class GetUserByIdTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        public GetUserByIdTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetUserByIdApi_Returns_Success_Response()
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
        public async Task GetUserByIdApi_Returns_BadRequest_Response_When_QueryParam_Is_Not_Sent(string requestParam)
        {
            string requestUri = string.Empty;
            requestUri = Constants.GetById;
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
