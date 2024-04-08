namespace TaskFlowHub.Core.Application.UseCases.Users.UserLogin.Inbounds;

public interface ILoginUserUseCase
{
    /// <summary>
    /// Executes the login of an user asynchronously.
    /// </summary>
    /// <param name="inbound">The inbound data for the login of an user.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method executes the login of an user with the given inbound data. It returns a task that represents the
    /// asynchronous operation. The task result indicates the outcome of the login operation.
    /// </remarks>
    Task ExecuteAsync(UserLoginInbound inbound, CancellationToken cancellationToken);

    /// <summary>
    /// Sets the outcome handler for the use case.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    /// <remarks>
    /// This method sets the outcome handler for the use case. The outcome handler is responsible for handling the outcome
    /// of the use case execution.
    /// </remarks>
    /// <seealso cref="IUserLoginOutcomeHandler"/>
    void SetOutcomeHandler(IUserLoginOutcomeHandler outcomeHandler);
}
