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
            var response = await _client.GetAsync(Constants.GetById);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetUserByIdApi_Returns_BadRequest_Response_When_QueryParam_Is_Not_Sent(string requestParam)
        {
            var requestUri = Constants.GetById;
            requestUri.Replace("{userId}", requestParam);
            var response = await _client.GetAsync("api/user/getById/");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
