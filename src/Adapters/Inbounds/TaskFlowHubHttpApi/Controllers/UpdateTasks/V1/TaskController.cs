namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Controllers.UpdateTasks.V1;

/// <summary>
/// Controller for the task update user task.
/// </summary>
/// <remarks>
/// This controller is responsible for updating an existing user task.
/// </remarks>
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Route("api/v1/tasks")]
public sealed class TaskController(ILogger<TaskController> logger)
    : ControllerBase, IUpdateTaskOutcomeHandler
{
    private IResult? _viewModel;

    private readonly ILogger<TaskController> _logger = logger;

    void IUpdateTaskOutcomeHandler.InvalidData(IDictionary<string, string[]> errors)
    {
        var message = "The task could not be updated due to invalid data.";
        var error = ApiResponse<ValidationProblemDetails>
            .CreateBadRequestValidationErrors(errors, HttpContext, message);

        _viewModel = Results.BadRequest(error);
    }

    void IUpdateTaskOutcomeHandler.TaskUpdated()
    {
        var message = "The task has been successfully updated.";
        var result = ApiResponse<object>.CreateSuccess(new {}, message);

        _viewModel = Results.Ok(result);
    }

    void IUpdateTaskOutcomeHandler.TaskNotFound()
    {
        var message = "The task could not be updated because it was not found.";
        var error = ApiResponse<ProblemDetails>.CreateNotFoundResource(HttpContext, message);

        _viewModel = Results.NotFound(error);
    }

    /// <summary>
    /// Updates a task in the system.
    /// </summary>
    /// <param name="taskId">The identifier of the task to update.</param>
    /// <param name="request">The data for the task update.</param>
    /// <param name="userContextAccessor">The accessor for the user context.</param>
    /// <param name="useCase">The use case for updating the task.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of the task update.</returns>
    /// <response code="200">The task has been successfully updated.</response>
    /// <response code="400">The task could not be updated due to invalid data.</response>
    /// <response code="404">The task could not be updated because it was not found.</response>
    /// <remarks>
    /// This endpoint allows the update of a task in the system.
    /// </remarks>
    /// <example>
    /// PUT /api/v1/tasks/{taskId}
    /// Header: Authorization: Bearer {token}
    /// {
    ///    "Title": "Task 1",
    ///    "Description": "Description of Task 1"
    ///    "Status": "InProgress"
    /// }
    /// </example>
    [Authorize]
    [HttpPut("{taskId}", Name = "UpdateTask")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status404NotFound)]
    public async Task<IResult> UpdateTaskAsync(
        [FromRoute] Guid taskId,
        [FromBody] UpdateTaskRequest request,
        [FromServices] IUserContextAccessor userContextAccessor,
        [FromServices] IUpdateTaskUseCase useCase,
        CancellationToken cancellationToken)
    {
        useCase.SetOutcomeHandler(this);

        var userId = userContextAccessor.GetUserId();

        var inbound = new UpdateTaskInbound(
            taskId,
            userId,
            request.Title,
            request.Description,
            request.Status);

        await useCase.ExecuteAsync(inbound, cancellationToken);

        return _viewModel!;
    }
}
