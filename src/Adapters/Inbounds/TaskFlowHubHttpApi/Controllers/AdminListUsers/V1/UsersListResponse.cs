namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.AdminListUsers.V1;

/// <summary>
/// Response for the user listing endpoint.
/// </summary>
/// <param name="Username">The username of the user.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="IsAdministrator">Whether the user is an administrator.</param>
/// <remarks>
/// This response is used to represent the users in the system.
/// </remarks>
public record UsersListResponse(string Username, string Email, bool IsAdministrator);
