namespace TDD_IntegrationTests
{
    public class HealthCheckTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public HealthCheckTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task HealthCheckApi_Returns_Healthy_Response()
        {
            var response = await _client.GetAsync(Constants.HealthCheckApi);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
