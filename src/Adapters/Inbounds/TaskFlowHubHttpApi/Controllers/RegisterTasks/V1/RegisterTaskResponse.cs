namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.RegisterTasks.V1;

/// <summary>
/// Represents the response for a successful task registration.
/// </summary>
/// <param name="Id">The unique identifier of the registered task.</param>
/// <remarks>
/// This type is used to represent the response for a successful task registration.
/// </remarks>
public record RegisterTaskResponse(Guid Id);
