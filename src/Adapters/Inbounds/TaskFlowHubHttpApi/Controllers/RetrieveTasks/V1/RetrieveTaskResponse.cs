namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.RetrieveTasks.V1;

/// <summary>
/// Represents the response for a successful retrieval of a task.
/// </summary>
/// <param name="Id">The unique identifier of the registered task.</param>
/// <param name="Title">The title of the registered task.</param>
/// <param name="Description">The description of the registered task.</param>
/// <remarks>
/// This response is returned when a task has been successfully retrieved.
/// </remarks>
public record RetrieveTaskResponse(Guid Id, string Title, string Description);
