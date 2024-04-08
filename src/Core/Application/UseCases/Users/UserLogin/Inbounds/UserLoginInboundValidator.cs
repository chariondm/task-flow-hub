namespace TaskFlowHub.Core.Application.UseCases.Users.UserLogin.Inbounds;

public sealed class UserLoginInboundValidator : AbstractValidator<UserLoginInbound>
{
    public UserLoginInboundValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least {MinLength} characters long.")
            .MaximumLength(50).WithMessage("Username must not exceed {MaxLength} characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least {MinLength} characters long.")
            .MaximumLength(100).WithMessage("Password must not exceed {MaxLength} characters.");
    }
}
