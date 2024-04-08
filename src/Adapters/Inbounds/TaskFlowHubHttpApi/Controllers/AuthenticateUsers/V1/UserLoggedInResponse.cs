namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.AuthenticateUsers.V1;

/// <summary>
/// The response data for a successful user login.
/// </summary>
/// <param name="UserId">The unique identifier of the user.</param>
/// <param name="Token">The authentication access token.</param>
/// <remarks>
/// This response data is returned when a user has been successfully authenticated.
/// </remarks>
public record UserLoggedInResponse(Guid UserId, string Token);
