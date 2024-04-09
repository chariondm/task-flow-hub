namespace TaskFlowHub.Core.Application.UseCases.Tasks.RetrieveTasks;

public class RetrieveTaskUseCase(IValidator<RetrieveTaskInbound> validator, IRetrieveTaskRepository repository)
    : IRetrieveTaskUseCase
{
    private IRetrieveTaskOutcomeHandler? _outcomeHandler;

    private readonly IValidator<RetrieveTaskInbound> _validator = validator;

    private readonly IRetrieveTaskRepository _repository = repository;

    public async Task ExecuteAsync(RetrieveTaskInbound inbound, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(inbound, cancellationToken);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.InvalidData(validationResult.ToDictionary());
            return;
        }

        var task = await _repository.RetrieveTaskAsync(inbound.Id, cancellationToken);

        if (task is null)
        {
            _outcomeHandler!.TaskNotFound();
            return;
        }

        if(!CanAccessTask(inbound, task))
        {
            _outcomeHandler!.AccessDenied();
            return;
        }

        _outcomeHandler!.TaskRetrieved(task);
    }

    public void SetOutcomeHandler(IRetrieveTaskOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
    }

    /// <summary>
    /// Determines whether the user can access the task.
    /// </summary>
    /// <param name="inbound">The inbound data for the task retrieval.</param>
    /// <param name="task">The task to access.</param>
    /// <returns><c>true</c> if the user can access the task or is an administrator; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method determines whether the user can access the task.
    /// </remarks>
    private static bool CanAccessTask(RetrieveTaskInbound inbound, FlowTask task)
    {
        return task.UserId == inbound.UserId || inbound.IsAdmin;
    }
}
