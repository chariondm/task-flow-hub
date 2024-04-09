namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.UpdateTasks.V1;

/// <summary>
/// Represents the request data for updating a task.
/// </summary>
/// <param name="Title">The title of the task.</param>
/// <param name="Description">The description of the task.</param>
/// <param name="Status">The status of the task.</param>
/// <remarks>
/// This record represents the request data for updating a task.
/// </remarks>
public record UpdateTaskRequest(string Title, string Description, FlowTaskStatus Status);
