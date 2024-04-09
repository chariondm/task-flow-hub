namespace TaskFlowHub.Core.Application.UseCases.Users.AdminListUsers;

public class AdminListUsersUseCase(IAdminListUsersRepository repository) : IAdminListUsersUseCase
{
    private IAdminListUsersOutcomeHandler? _outcomeHandler;

    private readonly IAdminListUsersRepository _repository = repository;

    public async Task ExecuteAsync(Guid requesterUserId, CancellationToken cancellationToken)
    {
        var isUserAnAdministrator = await _repository.IsUserAnAdministratorAsync(requesterUserId, cancellationToken);

        if (!isUserAnAdministrator)
        {
            _outcomeHandler!.UserIsNotAnAdministrator();
            return;
        }

        var users = await _repository.ListUsersAsync(cancellationToken);

        _outcomeHandler!.UsersListed(users);
    }

    public void SetOutcomeHandler(IAdminListUsersOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
    }
}
