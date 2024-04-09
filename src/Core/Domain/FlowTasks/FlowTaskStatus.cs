namespace TaskFlowHub.Core.Domain.FlowTasks;

/// <summary>
/// Represents the status of a flow task in the system
/// </summary>
/// <remarks>
/// This enumeration represents the status of a task. It is used to indicate the current status of a flow task in the system.
/// </remarks>
public enum FlowTaskStatus
{
    /// <summary>
    /// Represents a task that has been cancelled.
    /// </summary>
    Cancelled = 0,

    /// <summary>
    /// Represents a task that has been created.
    /// </summary>
    Created = 1,

    /// <summary>
    /// Represents a task that has been completed.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Represents a task that is in progress.
    /// </summary>
    InProgress = 3,

    /// <summary>
    /// Represents a task that is pending.
    /// </summary>
    Pending = 4
}
