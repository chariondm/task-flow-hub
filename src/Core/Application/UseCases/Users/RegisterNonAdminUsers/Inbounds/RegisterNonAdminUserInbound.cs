namespace TaskFlowHub.Core.Application.UseCases.Users.RegisterNonAdminUser.Inbounds;

/// <summary>
/// Represents the inbound data for the RegisterNonAdminUser use case.
/// </summary>
/// <param name="UserId">The identifier of the user to register.</param>
/// <param name="Username">The username of the user to register.</param>
/// <param name="Email">The email of the user to register.</param>
/// <param name="Password">The password of the user to register.</param>
/// <remarks>
/// This record represents the inbound data required for registering an user. It is used to pass the necessary
/// data to the use case for the registration of an user.
public record RegisterNonAdminUserInbound(Guid UserId, string Username, string Email, string Password);
