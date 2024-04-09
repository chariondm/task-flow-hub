namespace TaskFlowHub.Core.Application.UseCases.Tasks.UpdateTasks;

public class UpdateTaskUseCase(IValidator<UpdateTaskInbound> validator, IUpdateTaskRepository repository)
    : IUpdateTaskUseCase
{
    private IUpdateTaskOutcomeHandler? _outcomeHandler;

    private readonly IValidator<UpdateTaskInbound> _validator = validator;

    private readonly IUpdateTaskRepository _repository = repository;

    public async Task ExecuteAsync(UpdateTaskInbound inbound, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(inbound, cancellationToken);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.InvalidData(validationResult.ToDictionary());
            return;
        }

        var task = new FlowTask(
            inbound.Id,
            inbound.UserId,
            inbound.Title,
            inbound.Description,
            inbound.Status,
            DateTime.UtcNow);

        var rowsAffected = await _repository.UpdateTaskAsync(task, cancellationToken);

        if (rowsAffected == 0)
        {
            _outcomeHandler!.TaskNotFound();
            return;
        }

        _outcomeHandler!.TaskUpdated();
    }

    public void SetOutcomeHandler(IUpdateTaskOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
    }
}
