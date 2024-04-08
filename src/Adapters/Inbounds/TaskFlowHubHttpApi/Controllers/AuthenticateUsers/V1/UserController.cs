namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.AuthenticateUsers.V1;

/// <summary>
/// Controller for the user authentication endpoint.
/// </summary>
/// <remarks>
/// This controller allows the authentication of a user. The user must be registered and provide the correct password.
/// </remarks>
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Route("api/v1/auth")]
public sealed class AuthenticateUserController(ILogger<AuthenticateUserController> logger)
    : ControllerBase, IUserLoginOutcomeHandler
{
    private IResult? _viewModel;

    private readonly ILogger<AuthenticateUserController> _logger = logger;

    void IUserLoginOutcomeHandler.InvalidData(IDictionary<string, string[]> errors)
    {
        var message = "The user authentication could not be completed because the provided request data is invalid.";
        var error = ApiResponse<ValidationProblemDetails>
            .CreateBadRequestValidationErrors(errors, HttpContext, message);

        _viewModel = Results.BadRequest(error);
    }

    void IUserLoginOutcomeHandler.UserNotRegistered()
    {
        var message = "The user authentication could not be completed because the user is not registered.";
        var error = ApiResponse<ProblemDetails>
            .CreateNotFoundResource(HttpContext, message);

        _viewModel = Results.NotFound(error);
    }

    void IUserLoginOutcomeHandler.IncorrectPassword()
    {
        _viewModel = Results.Unauthorized();
    }

    void IUserLoginOutcomeHandler.UserLoggedIn(Guid userId, string token)
    {
        var message = "The user has been successfully authenticated.";
        var result = ApiResponse<UserLoggedInResponse>
            .CreateSuccess(new UserLoggedInResponse(userId, token), message);

        _viewModel = Results.Ok(result);
    }

    /// <summary>
    /// Authenticates a user.
    /// </summary>
    /// <param name="request">The request data for the user authentication.</param>
    /// <param name="useCase">The use case for authenticating a user.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of the user authentication.</returns>
    /// <response code="200">The user has been successfully authenticated.</response>
    /// <response code="400">The user could not be authenticated because the provided request data is invalid.</response>
    /// <response code="401">The user could not be authenticated because the password is incorrect.</response>
    /// <response code="404">The user could not be authenticated because the user is not registered.</response>
    /// <remarks>
    /// This endpoint allows the authentication of a user. The request data must be valid and the user must be registered.
    /// </remarks>
    /// <example>
    /// POST /api/v1/auth/login
    /// {
    ///    "username": "john.doe",
    ///    "password": "password"
    /// }
    /// </example>
    [HttpPost("login", Name = "LoginUser")]
    [ProducesResponseType(typeof(ApiResponse<UserLoggedInResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status404NotFound)]
    public async Task<IResult> LoginUserAsync(
        [FromBody] UserLoginRequest request,
        [FromServices] ILoginUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        useCase.SetOutcomeHandler(this);

        var inbound = new UserLoginInbound(request.Username, request.Password);

        await useCase.ExecuteAsync(inbound, cancellationToken);

        return _viewModel!;
    }
}
