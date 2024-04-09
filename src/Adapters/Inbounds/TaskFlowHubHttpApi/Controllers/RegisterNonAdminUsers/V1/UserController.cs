namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.RegisterNonAdminUsers.V1;

/// <summary>
/// Controller for the user registration endpoint.
/// </summary>
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Route("api/v1/users")]
public sealed class UserController(ILogger<UserController> logger)
    : ControllerBase, IRegisterNonAdminUserOutcomeHandler
{
    private IResult? _viewModel;

    private readonly ILogger<UserController> _logger = logger;

    void IRegisterNonAdminUserOutcomeHandler.InvalidData(IDictionary<string, string[]> errors)
    {
        var message = "The user registration could not be completed because the provided request data is invalid.";
        var error = ApiResponse<ValidationProblemDetails>
            .CreateBadRequestValidationErrors(errors, HttpContext, message);

        _viewModel = Results.BadRequest(error);
    }

    void IRegisterNonAdminUserOutcomeHandler.UserAlreadyRegistered()
    {
        var message = "The user registration could not be completed because the user is already registered.";
        var error = ApiResponse<ProblemDetails>
            .CreateConflictResourceError(HttpContext, detailUrl: null, message: message);
        
        _viewModel = Results.Conflict(error);
    }

    void IRegisterNonAdminUserOutcomeHandler.NonAdminUserRegistered(Guid userId)
    {
        var message = "The non-admin user has been successfully registered.";
        var result = ApiResponse<UserRegisteredResponse>
            .CreateSuccess(new UserRegisteredResponse(userId), message);

        _viewModel = Results.Created(string.Empty, result);
    }

    /// <summary>
    /// Registers a new non-admin user.
    /// </summary>
    /// <param name="request">The request data for the user registration.</param>
    /// <param name="useCase">The use case for registering a new non-admin user.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of the user registration.</returns>
    /// <response code="201">The non-admin user has been successfully registered.</response>
    /// <response code="400">The non-admin user could not be registered because the provided request data is invalid.</response>
    /// <response code="409">The non-admin user could not be registered because the user is already registered.</response>
    /// <remarks>
    /// This endpoint allows the registration of a new non-admin user. The request data must be valid and the user must
    /// not be already registered.
    /// </remarks>
    /// <example>
    /// POST /api/v1/users
    /// {
    ///     "username": "john.doe",
    ///     "email": "email@email.com",
    ///     "password": "password"
    ///  }
    ///  </example>
    [HttpPost(Name = "RegisterNonAdminUser")]
    [ProducesResponseType(typeof(ApiResponse<UserRegisteredResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status409Conflict)]
    public async Task<IResult> RegisterNonAdminUserAsync(
        [FromBody] UserRegistrationRequest request,
        [FromServices] IRegisterNonAdminUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        useCase.SetOutcomeHandler(this);

        var inbound = new RegisterNonAdminUserInbound(
            Guid.NewGuid(),
            request.Username,
            request.Email,
            request.Password);

        await useCase.ExecuteAsync(inbound, cancellationToken);

        return _viewModel!;
    }
}
