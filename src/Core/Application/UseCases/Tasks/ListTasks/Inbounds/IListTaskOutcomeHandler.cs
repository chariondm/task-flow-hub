namespace TaskFlowHub.Core.Application.UseCases.Tasks.ListTasks.Inbounds;

/// <summary>
/// Defines the contract for handling the outcome of the ListTask use case.
/// </summary>
public interface IListTaskOutcomeHandler
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
    /// Handles the scenario where the use case successfully lists the tasks.
    /// </summary>
    /// <param name="tasks">The list of tasks.</param>
    /// <remarks>
    /// This method is called when the use case successfully lists the tasks.
    /// </remarks>
    void TasksListed(IEnumerable<FlowTask> tasks);
}
