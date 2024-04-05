namespace TaskFlowHub.Core.Application.UseCases.Users.RegisterNonAdminUser.Inbounds;

/// <summary>
/// Defines the contract for handling the outcome of the RegisterNonAdminUser use case.
/// </summary>
public interface IRegisterNonAdminUserOutcomeHandler
{
    /// <summary>
    /// Handles the scenario where the use case detects that the inbound data is invalid.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    /// <remarks>
    /// This method is called when the use case detects that the inbound data is invalid.
    /// </remarks>
    void InvalidData(IDictionary<string, string[]> errors);

    /// <summary>
    /// Handles the scenario where the use case detects that the user is already registered.
    /// </summary>
    /// <remarks>
    /// This method is called when the use case detects that the username is already registered or the email is already in use.
    /// </remarks>
    void UserAlreadyRegistered();

    /// <summary>
    /// Handles the scenario where the use case successfully registers a non-admin user.
    /// </summary>
    /// <param name="userId">The identifier of the registered user.</param>
    /// <remarks>
    /// This method is called when the use case successfully registers a non-admin user.
    /// </remarks>
    void NonAdminUserRegistered(Guid userId);
}
