namespace TaskFlowHub.Core.Application.UseCases.Tasks.RetrieveTasks.Inbounds;

/// <summary>
/// Defines the contract for handling the outcome of the RetrieveTask use case.
/// </summary>
public interface IRetrieveTaskOutcomeHandler
{
    /// <summary>
    /// Handles the scenario where the use case detects that the user cannot access the task.
    /// </summary>
    /// <remarks>
    /// This method is called when the use case detects that the user cannot access the task.
    /// </remarks>
    void AccessDenied();

    /// <summary>
    /// Handles the scenario where the use case detects that the inbound data is invalid.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    /// <remarks>
    /// This method is called when the use case detects that the inbound data is invalid.
    /// </remarks>
    void InvalidData(IDictionary<string, string[]> errors);

    /// <summary>
    /// Handles the scenario where the use case detects that the task does not exist.
    /// </summary>
    /// <remarks>
    /// This method is called when the use case detects that the task does not exist.
    /// </remarks>
    void TaskNotFound();

    /// <summary>
    /// Handles the scenario where the use case successfully retrieves a task.
    /// </summary>
    /// <param name="task">The retrieved task.</param>
    /// <remarks>
    /// This method is called when the use case successfully retrieves a task.
    /// </remarks>
    /// <returns>The retrieved task.</returns>
    FlowTask TaskRetrieved(FlowTask task);
}
