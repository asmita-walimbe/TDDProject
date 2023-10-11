using FluentValidation;
using FluentValidation.Results;
using TDDProject.Interfaces;
using TDDProject.Models;
using TDDUnitTests.Constants;

namespace TDDUnitTests;

public class UserControllerTests
{
    private readonly Fixture _fixture;
    private readonly UserController _controller;
    private readonly Mock<IUserService> _mockuserService;
    private readonly Mock<IValidator<User>> _mockValidator;
    public UserControllerTests()
    {
        _fixture = new Fixture();
        _mockuserService = new Mock<IUserService>();
        _mockValidator = new Mock<IValidator<User>>();
        _controller = new UserController(_mockuserService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task GetByIdAsync_With_Correct_Request_Returns_Success_With_ResponseBody()
    {
        //Arrange
        var user = _fixture.Create<User>();
        var mockServiceResponse = _mockuserService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
           .ReturnsAsync(user);
        //Act
        var response = await _controller.GetByIdAsync(user.Id);
        // Assert
        response.Should().BeOfType<OkObjectResult>();
        var result = response as OkObjectResult;
        result.Value.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task GetByIdAsync_With_Correct_Request_Returns_Success_Without_ResponseBody()
    {
        //Arrange
        User user = null;
        var mockServiceResponse = _mockuserService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
           .ReturnsAsync(user);
        //Act
        var response = await _controller.GetByIdAsync(4);
        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(null)]
    public async Task GetByIdAsync_With_Incorrect_Request_Returns_Failure(int userId)
    {
        //Act
        var response = await _controller.GetByIdAsync(userId);
        // Assert
        response.Should().BeOfType<BadRequestObjectResult>();
        var result = response as BadRequestObjectResult;
        result.Value.Should().Be("UserId is required");
    }

    [Fact]
    public async Task AddUserAsync_With_Correct_Request_Returns_Success_With_ResponseBody()
    {
        //Arrange
        var user = _fixture.Create<User>();
        var _mockValidatorResponse = _mockValidator.Setup(v => v.Validate(It.IsAny<User>()))
            .Returns(new FluentValidation.Results.ValidationResult());
        var mockServiceResponse = _mockuserService.Setup(x => x.AddUserAsync(It.IsAny<User>()))
           .ReturnsAsync(user);
        //Act
        var response = await _controller.AddUserAsync(user);
        // Assert
        response.Should().BeOfType<CreatedAtActionResult>();
        var result = response as CreatedAtActionResult;
        result.Value.Should().BeEquivalentTo(user);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task AddUserAsync_With_Incorrect_Request_Returns_Failure(string property)
    {
        //Arrange
        var user = _fixture.Create<User>();
        user.Name = property;
        var _mockValidatorResponse = _mockValidator.Setup(v => v.Validate(It.IsAny<User>()))
               .Returns(new ValidationResult(
                   new List<ValidationFailure>() {
               new ValidationFailure(nameof(TestConstants.Name),TestConstants.NameError),
                new ValidationFailure(nameof(TestConstants.Address),TestConstants.AddressError),
               }));
        var mockServiceResponse = _mockuserService.Setup(x => x.AddUserAsync(It.IsAny<User>()))
           .ReturnsAsync(user);

        //Act
        var response = await _controller.AddUserAsync(user);
        // Assert
        response.Should().NotBeNull().And.BeOfType<BadRequestObjectResult>();
        var result = response as ObjectResult;
        result.Value.Should().BeAssignableTo<ValidationResult>();
        var errors = (ValidationResult)result.Value;
        errors.Errors.Should().Contain(x => x.ErrorMessage.Equals(TestConstants.NameError))
            .And.Contain(a => a.ErrorMessage.Equals(TestConstants.AddressError));

    }
}
