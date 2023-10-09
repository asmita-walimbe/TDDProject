using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using TDDProject;

namespace Integration
{
    public class HealthCheckIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        public HealthCheckIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task HealthCheckApi_Returns_Healthy_Response()
        {
            var response = await _client.GetAsync(Constants.HealthCheckApi);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
