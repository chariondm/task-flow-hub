namespace TaskFlowHub.Core.Domain.FlowTasks;

/// <summary>
/// Represents a task domain model in the system.
/// </summary>
/// <param name="Id">The unique identifier of the task.</param>
/// <param name="UserId">The unique identifier of the user who owns the task.</param>
/// <param name="Title">The title of the task.</param>
/// <param name="Description">The description of the task.</param>
/// <param name="Status">The status of the task.</param>
/// <param name="CreationDate">The date and time when the task was created.</param>
/// <returns>A task domain model.</returns>
/// <remarks>
/// This is a public record type that represents a task domain model in the system.
/// </remarks>
public record FlowTask(
    Guid Id,
    Guid UserId,
    string Title,
    string Description,
    FlowTaskStatus Status,
    DateTime CreationDate);
