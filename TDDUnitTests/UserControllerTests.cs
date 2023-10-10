using AutoFixture;
using TDDProject.Models;

namespace TDDUnitTests
{
    public class UserControllerTests
    {
        Fixture _fixture;
        private readonly UserController _controller;
        public UserControllerTests()
        {
            _fixture = new Fixture();
            _controller = new UserController();
        }

        [Fact]
        public async Task GetByIdAsync_With_Correct_Request_Returns_Success_With_ResponseBody()
        {
            //Arrange
            UserAutoFixture.GetUserFixture(_fixture);
            User user = _fixture.Create<User>();

            //Act
            var response = await _controller.GetByIdAsync(user.UserId);
            response.Should().BeOfType<OkObjectResult>();
            var result = response as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task GetByIdAsync_With_Correct_Request_Returns_Success_Without_ResponseBody()
        {
            //Arrange
            User user = _fixture.Create<User>();

            //Act
            var response = await _controller.GetByIdAsync(user.UserId);
            response.Should().BeOfType<NoContentResult>();
            var result = response as NoContentResult;

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public async Task GetByIdAsync_With_Incorrect_Request_Returns_Failure(int userId)
        {
            //Act
            var response = await _controller.GetByIdAsync(userId);
            response.Should().BeOfType<BadRequestObjectResult>();
            var result = response as BadRequestObjectResult;

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            var errors = result.Value as IEnumerable<string>;
            errors.Should().NotBeNull();
            errors.Should().Contain("UserId is required");
        }
    }
}

