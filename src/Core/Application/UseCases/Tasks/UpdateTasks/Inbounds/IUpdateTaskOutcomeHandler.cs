namespace TaskFlowHub.Core.Application.UseCases.Tasks.UpdateTasks.Inbounds;

/// <summary>
/// Defines the contract for handling the outcome of the UpdateTask use case.
/// </summary>
public interface IUpdateTaskOutcomeHandler
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
    /// Handles the scenario where the use case successfully updates the given task.
    /// </summary>
    /// <remarks>
    /// This method is called when the use case successfully updates the given task.
    /// </remarks>
    void TaskUpdated();

    /// <summary>
    /// Handles the scenario where the use case detects that the task to update was not found.
    /// </summary>
    /// <remarks>
    /// This method is called when the use case detects that the task to update was not found.
    /// </remarks>
    void TaskNotFound();
}
