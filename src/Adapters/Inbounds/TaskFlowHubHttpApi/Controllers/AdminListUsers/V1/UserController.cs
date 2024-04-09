namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.AdminListUsers.V1;

/// <summary>
/// Controller for the user listing endpoint.
/// </summary>
/// <remarks>
/// This controller is responsible for listing the users in the system.
/// </remarks>
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Route("api/v1/users")]
public sealed class UserController(ILogger<UserController> logger)
    : ControllerBase, IAdminListUsersOutcomeHandler
{
    private IResult? _viewModel;

    private readonly ILogger<UserController> _logger = logger;

    void IAdminListUsersOutcomeHandler.UserIsNotAnAdministrator()
    {
        _viewModel = Results.Forbid();
    }

    void IAdminListUsersOutcomeHandler.UsersListed(IEnumerable<User> users)
    {
        var message = "The users have been successfully listed.";
        var response = users.Select(user => new UsersListResponse(user.Username, user.Email, user.IsAdmin));
        var result = ApiResponse<IEnumerable<UsersListResponse>>
            .CreateSuccess(response, message);

        _viewModel = Results.Ok(result);
    }

    /// <summary>
    /// Lists the users in the system.
    /// </summary>
    /// <param name="userContextAccessor">The accessor for the user context.</param>
    /// <param name="useCase">The use case for listing the users.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of the user listing.</returns>
    /// <response code="200">The users have been successfully listed.</response>
    /// <response code="403">The users could not be listed because the requester is not an administrator.</response>
    /// <remarks>
    /// This endpoint allows the listing of the users in the system. The requester must be an administrator to access this
    /// endpoint.
    /// </remarks>
    /// <example>
    /// GET /api/v1/users
    /// </example>
    [Authorize(Roles = "admin")]
    [HttpGet(Name = "ListUsers")]
    [ProducesResponseType(typeof(IEnumerable<UsersListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IResult> ListUsersAsync(
        [FromServices] IUserContextAccessor userContextAccessor,
        [FromServices] IAdminListUsersUseCase useCase,
        CancellationToken cancellationToken)
    {
        useCase.SetOutcomeHandler(this);

        var userId = userContextAccessor.GetUserId();

        await useCase.ExecuteAsync(userId, cancellationToken);

        return _viewModel!;
    }
}
