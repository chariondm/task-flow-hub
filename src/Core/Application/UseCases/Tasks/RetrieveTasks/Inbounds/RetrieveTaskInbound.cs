namespace TaskFlowHub.Core.Application.UseCases.Tasks.RetrieveTasks.Inbounds;

/// <summary>
/// Represents the inbound data required for retrieving a task.
/// </summary>
/// <param name="Id">The identifier of the task to register.</param>
/// <param name="UserId">The identifier of the user that owns the task.</param>
/// <param name="IsAdmin">A value indicating whether the user is an administrator.</param>
/// <remarks>
/// This record represents the data required for the RetrieveTask use case.
/// </summary>
public record RetrieveTaskInbound(Guid Id, Guid UserId, bool IsAdmin);
