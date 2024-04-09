namespace TaskFlowHub.Core.Application.UseCases.Tasks.UpdateTasks.Outbounds;

/// <summary>
/// Defines the contract for the repository used by the UpdateTask use case.
/// </summary>
/// <remarks>
/// This interface defines the contract for the repository used by the UpdateTask use case. The repository is responsible
/// for handling the data access operations required by the use case.
/// </remarks>
public interface IUpdateTaskRepository
{
    /// <summary>
    /// Updates the task with the given data.
    /// </summary>
    /// <param name="task">The task to update.</param>]
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method updates the task with the given data.
    /// </remarks>
    Task<int> UpdateTaskAsync(FlowTask task, CancellationToken cancellationToken);
}
