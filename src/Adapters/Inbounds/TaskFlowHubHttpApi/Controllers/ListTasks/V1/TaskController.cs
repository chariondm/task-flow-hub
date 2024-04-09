namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.ListTasks.V1;

/// <summary>
/// Controller for the retrieval all user tasks.
/// </summary>
/// <remarks>
/// This controller is responsible for handling the retrieval of all user tasks in the system.
/// </remarks>
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Route("api/v1/tasks")]
public sealed class TaskController(ILogger<TaskController> logger)
    : ControllerBase, IListTaskOutcomeHandler
{
    private IResult? _viewModel;

    private readonly ILogger<TaskController> _logger = logger;

    void IListTaskOutcomeHandler.InvalidData(IDictionary<string, string[]> errors)
    {
        var message = "The task could not be retrieved due to invalid data.";
        var error = ApiResponse<ValidationProblemDetails>
            .CreateBadRequestValidationErrors(errors, HttpContext, message);

        _viewModel = Results.BadRequest(error);
    }

    void IListTaskOutcomeHandler.TasksListed(IEnumerable<FlowTask> tasks)
    {
        var message = "The tasks have been successfully retrieved.";
        var response = tasks.Select(task => new ListTaskResponse(task.Id, task.Title, task.Description));
        var result = ApiResponse<IEnumerable<ListTaskResponse>>
            .CreateSuccess(response, message);

        _viewModel = Results.Ok(result);
    }

    /// <summary>
    /// Retrieves all user tasks in the system.
    /// </summary>
    /// <param name="userContextAccessor">The accessor for the user context.</param>
    /// <param name="useCase">The use case for retrieving the task.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of the task retrieval.</returns>
    /// <response code="200">The task has been successfully retrieved.</response>
    /// <response code="400">The task could not be retrieved due to invalid data.</response>
    /// <remarks>
    /// This endpoint allows the retrieval of all user tasks in the system.
    /// </remarks>
    /// <example>
    /// GET /api/v1/tasks
    /// Authorization: Bearer {token}
    /// </example>
    [Authorize]
    [HttpGet(Name = "ListTask")]
    [ProducesResponseType(typeof(ApiResponse<ListTaskResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    public async Task<IResult> ListTaskAsync(
        [FromServices] IUserContextAccessor userContextAccessor,
        [FromServices] IListTaskUseCase useCase,
        CancellationToken cancellationToken)
    {
        useCase.SetOutcomeHandler(this);

        var userId = userContextAccessor.GetUserId();

        var isAdmin = userContextAccessor.IsAdmin();

        var inbound = new ListTaskInbound(userId, isAdmin);

        await useCase.ExecuteAsync(inbound, cancellationToken);

        return _viewModel!;
    }
}
