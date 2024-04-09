namespace TaskFlowHub.Core.Application.UseCases.Tasks.UpdateTasks.Inbounds;

public interface IUpdateTaskUseCase
{
    /// <summary>
    /// Executes the update task use case asynchronously.
    /// </summary>
    /// <param name="inbound">The inbound data for the task update.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method executes the update task use case asynchronously. It updates the task with the given data.
    /// </remarks>
    Task ExecuteAsync(UpdateTaskInbound inbound, CancellationToken cancellationToken);

    /// <summary>
    /// Sets the outcome handler for the use case.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    /// <remarks>
    /// This method sets the outcome handler for the use case. The outcome handler is responsible for handling the outcome
    /// of the use case execution.
    /// </remarks>
    /// <seealso cref="IUpdateTaskOutcomeHandler"/>
    void SetOutcomeHandler(IUpdateTaskOutcomeHandler outcomeHandler);
}
