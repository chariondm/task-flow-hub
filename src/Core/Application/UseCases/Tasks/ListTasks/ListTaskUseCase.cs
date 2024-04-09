namespace TaskFlowHub.Core.Application.UseCases.Tasks.ListTasks;

public class ListTaskUseCase(IValidator<ListTaskInbound> validator, IListTaskRepository repository)
    : IListTaskUseCase
{
    private IListTaskOutcomeHandler? _outcomeHandler;

    private readonly IValidator<ListTaskInbound> _validator = validator;

    private readonly IListTaskRepository _repository = repository;

    public async Task ExecuteAsync(ListTaskInbound inbound, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(inbound, cancellationToken);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.InvalidData(validationResult.ToDictionary());
            return;
        }

        var tasks = await RetrieveBasedOnRole(inbound, cancellationToken);

        _outcomeHandler!.TasksListed(tasks);
    }

    public void SetOutcomeHandler(IListTaskOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
    }

    /// <summary>
    /// Retrieves the tasks based on the user's role.
    /// </summary>
    /// <remarks>
    /// This method retrieves the tasks based on the user's role. When the user is an administrator, the method lists all tasks;
    /// otherwise, it lists only the tasks owned by the user.
    /// </remarks>
    private async Task<IEnumerable<FlowTask>> RetrieveBasedOnRole(
        ListTaskInbound inbound,
        CancellationToken cancellationToken)
    {
        return inbound.IsAdmin
            ? await _repository.RetrieveAllTasksAsync(cancellationToken)
            : await _repository.RetrieveTasksAsync(inbound.UserId, cancellationToken);
    }
}
