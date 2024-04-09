namespace TaskFlowHub.Core.Application.UseCases.Tasks.RegisterTasks.Inbounds;

public interface IRegisterTaskUseCase
{
    /// <summary>
    /// Executes the registration of a task asynchronously.
    /// </summary>
    /// <param name="inbound">The inbound data for the registration of a task.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method executes the registration of a task with the given inbound data. It returns a task that represents the
    /// asynchronous operation. The task result indicates the outcome of the registration operation.
    /// </remarks>
    Task ExecuteAsync(RegisterTaskInbound inbound, CancellationToken cancellationToken);

    /// <summary>
    /// Sets the outcome handler for the use case.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    /// <remarks>
    /// This method sets the outcome handler for the use case. The outcome handler is responsible for handling the outcome
    /// of the use case execution.
    /// </remarks>
    /// <seealso cref="IRegisterTaskOutcomeHandler"/>
    void SetOutcomeHandler(IRegisterTaskOutcomeHandler outcomeHandler);
}
