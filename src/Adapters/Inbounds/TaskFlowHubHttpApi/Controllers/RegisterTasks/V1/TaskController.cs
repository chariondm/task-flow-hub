namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.RegisterTasks.V1;

/// <summary>
/// Controller for the task registration endpoint.
/// </summary>
/// <remarks>
/// This controller is responsible for registering a task in the system.
/// </remarks>
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Route("api/v1/tasks")]
public sealed class TaskController(ILogger<TaskController> logger)
    : ControllerBase, IRegisterTaskOutcomeHandler
{
    private IResult? _viewModel;

    private readonly ILogger<TaskController> _logger = logger;

    void IRegisterTaskOutcomeHandler.InvalidData(IDictionary<string, string[]> errors)
    {
        var message = "The task could not be registered due to invalid data.";
        var error = ApiResponse<ValidationProblemDetails>
            .CreateBadRequestValidationErrors(errors, HttpContext, message);

        _viewModel = Results.BadRequest(error);
    }

    void IRegisterTaskOutcomeHandler.TaskRegistered(Guid taskId)
    {
        var message = "The task has been successfully registered.";
        var result = ApiResponse<RegisterTaskResponse>
            .CreateSuccess(new RegisterTaskResponse(taskId), message);

        // TODO: Implement the CreatedAtRoute method
        // _viewModel = Results.CreatedAtRoute("GetTask", new { taskId }, result);
        _viewModel = Results.Created(string.Empty, result);
    }

    /// <summary>
    /// Registers a task in the system.
    /// </summary>
    /// <param name="request">The data for the task registration.</param>
    /// <param name="userContextAccessor">The accessor for the user context.</param>
    /// <param name="useCase">The use case for registering the task.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of the task registration.</returns>
    /// <response code="201">The task has been successfully registered.</response>
    /// <response code="400">The task could not be registered due to invalid data.</response>
    /// <remarks>
    /// This endpoint allows the registration of a task in the system.
    /// </remarks>
    /// <example>
    /// POST /api/v1/tasks
    /// Header: Authorization: Bearer {token}
    /// {
    ///  "Title": "Task 1",
    ///  "Description": "Description of Task 1"
    ///  }
    ///  </example>
    [Authorize]
    [HttpPost(Name = "RegisterTask")]
    [ProducesResponseType(typeof(ApiResponse<RegisterTaskResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    public async Task<IResult> RegisterTaskAsync(
        [FromBody] RegisterTaskRequest request,
        [FromServices] IUserContextAccessor userContextAccessor,
        [FromServices] IRegisterTaskUseCase useCase,
        CancellationToken cancellationToken)
    {
        useCase.SetOutcomeHandler(this);

        var userId = userContextAccessor.GetUserId();

        var inbound = new RegisterTaskInbound(
            Guid.NewGuid(),
            userId,
            request.Title,
            request.Description,
            DateTime.UtcNow);

        await useCase.ExecuteAsync(inbound, cancellationToken);

        return _viewModel!;
    }
}
