namespace TaskFlowHub.Core.Application.UseCases.Users.AdminListUsers.Outbounds
{
    /// <summary>
    /// Defines the contract for the repository used by the AdminListUsers use case.
    /// </summary>
    /// <remarks>
    /// This interface defines the contract for the repository used by the AdminListUsers use case. The repository is responsible
    /// for handling the data access operations required by the use case.
    /// </remarks>
    public interface IAdminListUsersRepository
    {
        /// <summary>
        /// Lists the users asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method lists the users asynchronously. It returns a task that represents the asynchronous operation. The task result
        /// indicates the list of users.
        /// </remarks>
        Task<IEnumerable<User>> ListUsersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Checks if the user is an administrator.
        /// </summary>
        /// <param name="userId">The user identifier to check.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method checks if the user is an administrator. It returns a task that represents the asynchronous operation. The task
        /// result indicates whether the user is an administrator.
        /// </remarks>
        Task<bool> IsUserAnAdministratorAsync(Guid userId, CancellationToken cancellationToken);
    }
}
