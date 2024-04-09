namespace TaskFlowHub.Core.Application.UseCases.Tasks.ListTasks.Inbounds;

/// <summary>
/// Represents the inbound data required for listing all tasks.
/// </summary>
/// <param name="UserId">The identifier of the user that owns the task.</param>
/// <param name="IsAdmin">A value indicating whether the user is an administrator.</param>
/// <remarks>
/// This record represents the data required for the ListTask use case. It contains the identifier of the task to list,
/// the identifier of the user that owns the task, and a value indicating whether the user is an administrator. When the
/// user is an administrator, the use case lists all tasks; otherwise, it lists only the tasks owned by the user.
/// </summary>
public record ListTaskInbound(Guid UserId, bool IsAdmin);
