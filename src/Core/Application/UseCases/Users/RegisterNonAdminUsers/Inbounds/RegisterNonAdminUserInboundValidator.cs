namespace TaskFlowHub.Core.Application.UseCases.Users.RegisterNonAdminUser.Inbounds;

public class RegisterNonAdminUserInboundValidator : AbstractValidator<RegisterNonAdminUserInbound>
{
    public RegisterNonAdminUserInboundValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least {MinLength} characters long.")
            .MaximumLength(50).WithMessage("Username must not exceed {MaxLength} characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.")
            .MaximumLength(255).WithMessage("Email must not exceed {MaxLength} characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least {MinLength} characters long.")
            .MaximumLength(100).WithMessage("Password must not exceed {MaxLength} characters.");
    }
}
