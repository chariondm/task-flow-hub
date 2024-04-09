namespace TaskFlowHub.Core.Application.UseCases.Tasks.ListTasks.Outbounds;

/// <summary>
/// Defines the contract for the repository used by the ListTask use case.
/// </summary>
/// <remarks>
/// This interface defines the contract for the repository used by the ListTask use case. The repository is responsible
/// for handling the data access operations required by the use case.
/// </remarks>
public interface IListTaskRepository
{   
    /// <summary>
    /// Retrieves all user tasks asynchronously.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves all user tasks asynchronously.
    /// </remarks>
    Task<IEnumerable<FlowTask>> RetrieveTasksAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all tasks of all users asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves all tasks of all users asynchronously. This method must be called only when the user is
    /// an administrator.
    /// </remarks>
    Task<IEnumerable<FlowTask>> RetrieveAllTasksAsync(CancellationToken cancellationToken);
}
