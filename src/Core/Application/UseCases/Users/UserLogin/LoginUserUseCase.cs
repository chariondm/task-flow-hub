namespace TaskFlowHub.Core.Application.UseCases.Users.UserLogin;

public class LoginUserUseCase(
    IValidator<UserLoginInbound> validator,
    IUserLoginRepository repository,
    IUserLoginTokenGenerator tokenGenerator) : ILoginUserUseCase
{
    private IUserLoginOutcomeHandler? _outcomeHandler;

    private readonly IValidator<UserLoginInbound> _validator = validator;

    private readonly IUserLoginRepository _repository = repository;

    private readonly IUserLoginTokenGenerator _tokenGenerator = tokenGenerator;

    public async Task ExecuteAsync(UserLoginInbound inbound, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(inbound, cancellationToken);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.InvalidData(validationResult.ToDictionary());
            return;
        }

        var user = await _repository.GetUserByUsernameAsync(inbound.Username, cancellationToken);

        if (user is null)
        {
            _outcomeHandler!.UserNotRegistered();
            return;
        }

        if (!VerifyPassword(inbound.Password, user.Password))
        {
            _outcomeHandler!.IncorrectPassword();
            return;
        }

        var token = _tokenGenerator.GenerateToken(user);

        await _repository.UpdateLastLoginDateAsync(user.Id, DateTime.UtcNow, cancellationToken);

        _outcomeHandler!.UserLoggedIn(user.Id, token);
    }

    public void SetOutcomeHandler(IUserLoginOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
    }

    private static bool VerifyPassword(string password, string hashedPassword)
    {
        return Verify(password, hashedPassword);
    }
}
