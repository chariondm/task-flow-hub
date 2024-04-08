namespace TaskFlowHub.Core.Application.UseCases.Users.UserLogin.Outbounds;

/// <summary>
/// Represents the repository for user login operations.
/// </summary>
/// <remarks>
/// This interface represents the repository for user login operations. It provides methods to retrieve and update user login
/// information.
/// </remarks>
public interface IUserLoginRepository
{
    /// <summary>
    /// Retrieves the user with the specified username asynchronously.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves the user with the specified username. It returns a task that represents the asynchronous operation.
    /// The task result contains the user with the specified username, or <see langword="null"/> if no user was found.
    /// </remarks>
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the last login date of the user with the specified identifier asynchronously.
    /// </summary>
    /// <param name="userId">The identifier of the user to update.</param>
    /// <param name="lastLoginDate">The last login date to set.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method updates the last login date of the user with the specified identifier. It returns a task that represents the
    /// asynchronous operation. The task result contains the number of affected rows.
    /// </remarks>
    Task<int> UpdateLastLoginDateAsync(Guid userId, DateTime lastLoginDate, CancellationToken cancellationToken);
}
