namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.RegisterNonAdminUsers.V1;

/// <summary>
/// Represents the request data for registering a new user.
/// </summary>
/// <param name="Username">The unique username of the user to register.</param>
/// <param name="Email">The unique email address of the user to register.</param>
/// <param name="Password">The password of the user to register.</param>
/// <remarks>
/// This type is used to represent the request data for registering a new user.
/// </remarks>
public record UserRegistrationRequest(string Username, string Email, string Password);
