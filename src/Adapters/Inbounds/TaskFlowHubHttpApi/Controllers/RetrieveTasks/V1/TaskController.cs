namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.RetrieveTasks.V1;

/// <summary>
/// Controller for the retrieval of tasks.
/// </summary>
/// <remarks>
/// This controller is responsible for the retrieval of tasks.
/// </remarks>
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Route("api/v1/tasks")]
public sealed class TaskController(ILogger<TaskController> logger)
    : ControllerBase, IRetrieveTaskOutcomeHandler
{
    private IResult? _viewModel;

    private readonly ILogger<TaskController> _logger = logger;

    void IRetrieveTaskOutcomeHandler.AccessDenied()
    {
        _viewModel = Results.Forbid();
    }

    void IRetrieveTaskOutcomeHandler.InvalidData(IDictionary<string, string[]> errors)
    {
        var message = "The task could not be retrieved due to invalid data.";
        var error = ApiResponse<ValidationProblemDetails>
            .CreateBadRequestValidationErrors(errors, HttpContext, message);

        _viewModel = Results.BadRequest(error);
    }

    void IRetrieveTaskOutcomeHandler.TaskNotFound()
    {
        var message = "The task could not be found.";
        var error = ApiResponse<ProblemDetails>
            .CreateNotFoundResource(HttpContext, message);

        _viewModel = Results.NotFound(error);
    }

    FlowTask IRetrieveTaskOutcomeHandler.TaskRetrieved(FlowTask task)
    {
        var message = "The task has been successfully retrieved.";
        var response = new RetrieveTaskResponse(task.Id, task.Title, task.Description);
        var result = ApiResponse<RetrieveTaskResponse>
            .CreateSuccess(response, message);

        _viewModel = Results.Ok(result);

        return task;
    }

    /// <summary>
    /// Retrieves a task in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the task to retrieve.</param>
    /// <param name="userContextAccessor">The accessor for the user context.</param>
    /// <param name="useCase">The use case for retrieving the task.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of the task retrieval.</returns>
    /// <response code="200">The task has been successfully retrieved.</response>
    /// <response code="400">The task could not be retrieved due to invalid data.</response>
    /// <response code="403">The task could not be retrieved because the requester does not have access to the task.</response>
    /// <response code="404">The task could not be retrieved because it could not be found.</response>
    /// <remarks>
    /// This endpoint allows the retrieval of a task in the system.
    /// </remarks>
    /// <example>
    /// GET /api/v1/tasks/{id}
    /// Authorization: Bearer {token}
    /// </example>
    [Authorize]
    [HttpGet("{id}", Name = "RetrieveTask")]
    [ProducesResponseType(typeof(ApiResponse<RetrieveTaskResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status404NotFound)]
    public async Task<IResult> RetrieveTaskAsync(
        [FromRoute] Guid id,
        [FromServices] IUserContextAccessor userContextAccessor,
        [FromServices] IRetrieveTaskUseCase useCase,
        CancellationToken cancellationToken)
    {
        useCase.SetOutcomeHandler(this);

        var userId = userContextAccessor.GetUserId();

        var isAdmin = userContextAccessor.IsAdmin();

        var inbound = new RetrieveTaskInbound(id, userId, isAdmin);

        await useCase.ExecuteAsync(inbound, cancellationToken);

        return _viewModel!;
    }
}
