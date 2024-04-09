namespace TaskFlowHub.Core.Application.UseCases.Tasks.RegisterTasks.Inbounds;

/// <summary>
/// Register Task Inbound
/// </summary>
/// <param name="Id">The identifier of the task to register.</param>
/// <param name="UserId">The identifier of the user that owns the task.</param>
/// <param name="Title">The title of the task to register.</param>
/// <param name="Description">The description of the task to register.</param>
/// <param name="CreationDate">The creation date of the task to register.</param>
/// <remarks>
/// This record represents the inbound data required for registering a task. It is used to pass the necessary
/// data to the use case for the registration of a task.
/// </summary>
public record RegisterTaskInbound(
    Guid Id,
    Guid UserId,
    string Title,
    string Description,
    DateTime CreationDate);
