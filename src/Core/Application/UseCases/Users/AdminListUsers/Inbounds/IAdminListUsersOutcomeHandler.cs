namespace TaskFlowHub.Core.Application.UseCases.Users.AdminListUsers.Inbounds;

/// <summary>
/// Defines the contract for handling the outcome of the AdminListUsers use case.
/// </summary>
public interface IAdminListUsersOutcomeHandler
{
    /// <summary>
    /// Handles the scenario where the use case successfully lists the users.
    /// </summary>
    /// <param name="users">The list of users.</param>
    /// <remarks>
    /// This method is called when the use case successfully lists the users.
    /// </remarks>
    void UsersListed(IEnumerable<User> users);

    /// <summary>
    /// Handles the scenario where the use case fails because the requester is not an administrator.
    /// </summary>
    /// <remarks>
    /// This method is called when the use case fails because the requester is not an administrator.
    /// </remarks>
    void UserIsNotAnAdministrator();
}
