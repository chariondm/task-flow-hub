namespace TaskFlowHub.Core.Application.UseCases.Users.UserLogin.Inbounds;

public interface IUserLoginOutcomeHandler
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
    /// Handles the scenario where the use case detects that the user is not registered.
    /// </summary>
    /// <remarks>
    /// This method is called when the use case detects that the username is not registered.
    /// </remarks>
    void UserNotRegistered();

    /// <summary>
    /// Handles the scenario where the use case detects that the password is incorrect.
    /// </summary>
    /// <remarks>
    /// This method is called when the use case detects that the password is incorrect.
    /// </remarks>
    void IncorrectPassword();

    /// <summary>
    /// Handles the scenario where the use case successfully logs in an user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="token">The generated token.</param>
    /// <remarks>
    /// This method is called when the use case successfully logs in an user.
    /// </remarks>
    void UserLoggedIn(Guid userId, string token);
}
