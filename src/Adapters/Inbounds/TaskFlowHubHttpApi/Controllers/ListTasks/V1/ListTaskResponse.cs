namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.ListTasks.V1;

/// <summary>
/// Represents the response for a successfully retrieved all user tasks.
/// </summary>
/// <param name="Id">The unique identifier of the registered task.</param>
/// <param name="Title">The title of the registered task.</param>
/// <param name="Description">The description of the registered task.</param>
/// <remarks>
/// This response represents the data returned when a task is successfully retrieved.
/// </remarks>
public record ListTaskResponse(Guid Id, string Title, string Description);
