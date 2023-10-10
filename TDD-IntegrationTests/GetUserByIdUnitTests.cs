using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TDDProject.Controllers;

namespace TDD_IntegrationTests
{
    public class GetUserByIdUnitTests
    {
        private readonly UserController _controller;
        public GetUserByIdUnitTests()
        {
            _controller = new UserController();
        }

        [Fact]
        public async Task GetUserById_Returns_Success()
        {
            var mockResponse = UserMockResponse.GetUserMockResponse();

            //Act
            var response = await _controller.GetById(1);
            var okResult = response as OkObjectResult;

            // Assert
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(mockResponse);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public async Task GetUserById_Returns_Failure(int userId)
        {
            //Act
            var response = await _controller.GetById(userId);
            var badRequestResult = response as BadRequestResult;

            // Assert
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
