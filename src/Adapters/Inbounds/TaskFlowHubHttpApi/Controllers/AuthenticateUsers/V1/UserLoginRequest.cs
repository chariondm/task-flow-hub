namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.AuthenticateUsers.V1;

/// <summary>
/// The request data for authenticating a user.
/// </summary>
/// <param name="Username">The username of the user.</param>
/// <param name="Password">The password of the user.</param>
/// <remarks>
/// This request data is used to authenticate a user. The username and password must be provided.
/// </remarks>
public record UserLoginRequest(string Username, string Password);
