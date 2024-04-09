namespace TaskFlowHub.Core.Application.UseCases.Tasks.ListTasks.Inbounds;

public interface IListTaskUseCase
{
    /// <summary>
    /// Executes the retrieval of all user tasks asynchronously.
    /// </summary>
    /// <param name="inbound">The inbound data for the task retrieval.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method executes the retrieval of all user tasks asynchronously. When the user is an administrator, the use case
    /// lists all tasks; otherwise, it lists only the tasks owned by the user.
    /// </remarks>
    Task ExecuteAsync(ListTaskInbound inbound, CancellationToken cancellationToken);

    /// <summary>
    /// Sets the outcome handler for the use case.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    /// <remarks>
    /// This method sets the outcome handler for the use case. The outcome handler is responsible for handling the outcome
    /// of the use case execution.
    /// </remarks>
    /// <seealso cref="IListTaskOutcomeHandler"/>
    void SetOutcomeHandler(IListTaskOutcomeHandler outcomeHandler);
}
