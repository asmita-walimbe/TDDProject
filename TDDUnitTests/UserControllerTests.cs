namespace TDDUnitTests
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        public UserControllerTests()
        {
            _controller = new UserController();
        }

        [Fact]
        public async Task GetUserById_With_Correct_Request_Returns_Success()
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
        public async Task GetUserById_With_Incorrect_Request_Returns_Failure(int userId)
        {
            //Act
            var response = await _controller.GetById(userId);
            var badRequestResult = response as BadRequestResult;

            // Assert
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}

