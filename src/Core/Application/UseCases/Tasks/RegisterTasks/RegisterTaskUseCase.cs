namespace TaskFlowHub.Core.Application.UseCases.Tasks.RegisterTasks;

public class RegisterTaskUseCase(IValidator<RegisterTaskInbound> validator, IRegisterTaskRepository repository)
    : IRegisterTaskUseCase
{
    private IRegisterTaskOutcomeHandler? _outcomeHandler;

    private readonly IValidator<RegisterTaskInbound> _validator = validator;

    private readonly IRegisterTaskRepository _repository = repository;

    public async Task ExecuteAsync(RegisterTaskInbound inbound, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(inbound, cancellationToken);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.InvalidData(validationResult.ToDictionary());
            return;
        }

        var task = new FlowTask(
            Guid.NewGuid(),
            inbound.UserId,
            inbound.Title,
            inbound.Description,
            FlowTaskStatus.Created,
            DateTime.UtcNow);

        await _repository.RegisterTaskAsync(task, cancellationToken);

        _outcomeHandler!.TaskRegistered(task.Id);
    }

    public void SetOutcomeHandler(IRegisterTaskOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
    }
}
