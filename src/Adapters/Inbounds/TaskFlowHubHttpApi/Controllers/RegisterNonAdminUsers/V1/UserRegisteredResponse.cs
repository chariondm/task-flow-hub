namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.RegisterNonAdminUsers.V1;

/// <summary>
/// Represents the response for a successful user registration.
/// </summary>
/// <param name="Id">The unique identifier of the registered user.</param>
/// <remarks>
/// This type is used to represent the response for a successful user registration.
/// </remarks>
public record UserRegisteredResponse(Guid Id);
