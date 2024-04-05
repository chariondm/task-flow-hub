namespace TaskFlowHub.Core.Application.UseCases.Users.RegisterNonAdminUser.Outbounds;

/// <summary>
/// Defines the contract for the repository used by the RegisterNonAdminUser use case.
/// </summary>
/// <remarks>
/// This interface defines the contract for the repository used by the RegisterNonAdminUser use case. The repository is responsible
/// for handling the data access operations required by the use case.
/// </remarks>
public interface IRegisterNonAdminUserUseRepository
{
    /// <summary>
    /// Checks if the username or email is already registered.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <param name="email">The email to check.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method checks if the username or email is already registered. It returns a task that represents the asynchronous
    /// operation. The task result indicates whether the username or email is already registered.
    /// </remarks>
    Task<bool> IsUsernameOrEmailRegisteredAsync(string username, string email, CancellationToken cancellationToken);

    /// <summary>
    /// Registers a new non-admin user.
    /// </summary>
    /// <param name="User">The user to register.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method registers a new non-admin user. It returns a task that represents the asynchronous operation. The task result
    /// indicates the number of affected rows in the data store. If the operation is successful, the result should be greater
    /// than zero.
    /// </remarks>
    Task<int> RegisterNonAdminUserAsync(User user, CancellationToken cancellationToken);
}
