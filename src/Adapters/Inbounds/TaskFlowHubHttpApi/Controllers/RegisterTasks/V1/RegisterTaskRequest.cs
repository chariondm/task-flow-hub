namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.RegisterTasks.V1;

/// <summary>
/// Represents the request data for registering a new task.
/// </summary>
/// <param name="Title">The title of the task to register.</param>
/// <param name="Description">The description of the task to register.</param>
/// <remarks>
/// This type is used to represent the request data for registering a new task.
/// </remarks>
public record RegisterTaskRequest(string Title, string Description);
