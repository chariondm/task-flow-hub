namespace TaskFlowHub.Core.Domain.Users;

/// <summary>
/// Represents a user domain model in the system.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="Username">The unique username of the user.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="Password">The password of the user.</param>
/// <param name="IsAdmin">A flag indicating whether the user is an admin.</param>
/// <returns>A user domain model.</returns>
/// <remarks>
/// This is a public record type that represents a user domain model in the system.
/// </remarks>
public record User(Guid Id, string Username, string Email, string Password, bool IsAdmin = false);
