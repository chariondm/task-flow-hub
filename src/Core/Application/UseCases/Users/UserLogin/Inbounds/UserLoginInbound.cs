namespace TaskFlowHub.Core.Application.UseCases.Users.UserLogin.Inbounds;

/// <summary>
/// Represents the inbound data for the UserLogin use case.
/// </summary>
/// <param name="Username">The username of the user to login.</param>
/// <param name="Password">The password of the user to login.</param>
/// <remarks>
/// This record represents the inbound data required for logging in an user. It is used to pass the necessary
/// data to the use case for the login of an user.
/// </remarks>
public record UserLoginInbound(string Username, string Password);
