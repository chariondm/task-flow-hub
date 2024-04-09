namespace TaskFlowHub.Core.Application.UseCases.Tasks.RegisterTasks.Inbounds;

/// <summary>
/// Defines the contract for handling the outcome of the RegisterTask use case.
/// </summary>
public interface IRegisterTaskOutcomeHandler
{
    /// <summary>
    /// Handles the scenario where the use case detects that the inbound data is invalid.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    /// <remarks>
    /// This method is called when the use case detects that the inbound data is invalid.
    /// </remarks>
    void InvalidData(IDictionary<string, string[]> errors);

    /// <summary>
    /// Handles the scenario where the use case successfully registers a task.
    /// </summary>
    /// <param name="taskId">The identifier of the registered task.</param>
    /// <remarks>
    /// This method is called when the use case successfully registers a task.
    /// </remarks>
    void TaskRegistered(Guid taskId);
}
