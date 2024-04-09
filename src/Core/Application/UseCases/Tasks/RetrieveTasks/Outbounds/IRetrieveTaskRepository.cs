namespace TaskFlowHub.Core.Application.UseCases.Tasks.RetrieveTasks.Outbounds;

/// <summary>
/// Defines the contract for the repository used by the RetrieveTask use case.
/// </summary>
/// <remarks>
/// This interface defines the contract for the repository used by the RetrieveTask use case. The repository is responsible
/// for handling the data access operations required by the use case.
/// </remarks>
public interface IRetrieveTaskRepository
{   
    /// <summary>
    /// Retrieves a task by its identifier.
    /// </summary>
    /// <param name="taskId">The identifier of the task to retrieve.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves a task by its identifier.
    /// </remarks>
    Task<FlowTask?> RetrieveTaskAsync(Guid taskId, CancellationToken cancellationToken);
}
