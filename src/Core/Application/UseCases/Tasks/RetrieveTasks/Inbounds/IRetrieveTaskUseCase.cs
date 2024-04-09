namespace TaskFlowHub.Core.Application.UseCases.Tasks.RetrieveTasks.Inbounds;

public interface IRetrieveTaskUseCase
{
    /// <summary>
    /// Executes the retrieval of a task asynchronously.
    /// </summary>
    /// <param name="inbound">The inbound data for the task retrieval.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method executes the retrieval of a task asynchronously.
    /// </remarks>
    Task ExecuteAsync(RetrieveTaskInbound inbound, CancellationToken cancellationToken);

    /// <summary>
    /// Sets the outcome handler for the use case.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    /// <remarks>
    /// This method sets the outcome handler for the use case. The outcome handler is responsible for handling the outcome
    /// of the use case execution.
    /// </remarks>
    /// <seealso cref="IRetrieveTaskOutcomeHandler"/>
    void SetOutcomeHandler(IRetrieveTaskOutcomeHandler outcomeHandler);
}
