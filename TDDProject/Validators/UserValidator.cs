using FluentValidation;
using TDDProject.Models;
using static TDDProject.Constants;

namespace TDDProject.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(StatusCodes.Status400BadRequest.ToString())
            .WithMessage(UserValidationMessage.Name);

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithErrorCode(StatusCodes.Status400BadRequest.ToString())
            .WithMessage(UserValidationMessage.Address);

    }
}
