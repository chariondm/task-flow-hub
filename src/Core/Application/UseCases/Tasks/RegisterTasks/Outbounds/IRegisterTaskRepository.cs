namespace TaskFlowHub.Core.Application.UseCases.Tasks.RegisterTasks.Outbounds;

/// <summary>
/// Defines the contract for the repository used by the RegisterTask use case.
/// </summary>
/// <remarks>
/// This interface defines the contract for the repository used by the RegisterTask use case. The repository is responsible
/// for handling the data access operations required by the use case.
/// </remarks>
public interface IRegisterTaskRepository
{
    /// <summary>
    /// Registers a new task.
    /// </summary>
    /// <param name="task">The task to register.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method registers a new task. It returns a task that represents the asynchronous operation. The task result
    /// indicates the number of affected rows in the data store. If the operation is successful, the result should be greater
    /// than zero.
    /// </remarks>
    Task<int> RegisterTaskAsync(FlowTask task, CancellationToken cancellationToken);
}
