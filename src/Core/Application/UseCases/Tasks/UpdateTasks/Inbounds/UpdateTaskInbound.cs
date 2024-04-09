namespace TaskFlowHub.Core.Application.UseCases.Tasks.UpdateTasks.Inbounds;

/// <summary>
/// Represents the inbound data for updating a task.
/// </summary>
/// <param name="Id">The identifier of the task to update.</param>
/// <param name="UserId">The identifier of the user updating the task.</param>
/// <param name="Title">The title of the task.</param>
/// <param name="Description">The description of the task.</param>
/// <param name="Status">The status of the task.</param>
/// <returns>The inbound data for updating a task.</returns>
/// <remarks>
/// This record represents the inbound data for updating a task.
/// </remarks>
public record UpdateTaskInbound(
    Guid Id,
    Guid UserId,
    string Title,
    string Description,
    FlowTaskStatus Status);
