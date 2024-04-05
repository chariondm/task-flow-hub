namespace TaskFlowHub.Core.Application.UseCases.Users.RegisterNonAdminUser;

public class RegisterNonAdminUserUseCase(IValidator<RegisterNonAdminUserInbound> validator, IRegisterNonAdminUserUseRepository repository)
    : IRegisterNonAdminUserUseCase

{
    private IRegisterNonAdminUserOutcomeHandler? _outcomeHandler;

    private readonly IValidator<RegisterNonAdminUserInbound> _validator = validator;

    private readonly IRegisterNonAdminUserUseRepository _repository = repository;

    public async Task ExecuteAsync(RegisterNonAdminUserInbound inbound, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(inbound, cancellationToken);

        if (!validationResult.IsValid)
        {
            _outcomeHandler?.InvalidData(validationResult.ToDictionary());
            return;
        }

        var userExists = await _repository
            .IsUsernameOrEmailRegisteredAsync(inbound.Username, inbound.Email, cancellationToken);

        if (userExists)
        {
            _outcomeHandler?.UserAlreadyRegistered();
            return;
        }

        var user = new User(inbound.UserId, inbound.Username, inbound.Email, HashPassword(inbound.Password));

        var affectedRows = await _repository.RegisterNonAdminUserAsync(user, cancellationToken);

        if (affectedRows == 0)
        {
            _outcomeHandler?.UserAlreadyRegistered();
            return;
        }

        _outcomeHandler?.NonAdminUserRegistered(user.Id);
    }

    public void SetOutcomeHandler(IRegisterNonAdminUserOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
    }
}
