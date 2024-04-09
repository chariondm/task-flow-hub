namespace TaskFlowHub.Core.Application.UseCases.Users.AdminListUsers.Inbounds;

public interface IAdminListUsersUseCase
{
    /// <summary>
    /// Executes the listing of users for an administrator asynchronously.
    /// </summary>
    /// <param name="requesterUserId">The user ID of the requester.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method executes the listing of users for an administrator. It returns a task that represents the asynchronous operation.
    /// The task result indicates the outcome of the listing operation.
    /// </remarks>
    Task ExecuteAsync(Guid requesterUserId, CancellationToken cancellationToken);

    /// <summary>
    /// Sets the outcome handler for the use case.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    /// <remarks>
    /// This method sets the outcome handler for the use case. The outcome handler is responsible for handling the outcome
    /// of the use case execution.
    /// </remarks>
    /// <seealso cref="IAdminListUsersOutcomeHandler"/>
    void SetOutcomeHandler(IAdminListUsersOutcomeHandler outcomeHandler);
}
