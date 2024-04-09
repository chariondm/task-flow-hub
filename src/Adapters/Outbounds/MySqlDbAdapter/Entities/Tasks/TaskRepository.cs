namespace TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Entities.Tasks;

public class TaskRepository(IDbConnectionFactory dbConnectionFactory, ILogger<TaskRepository> logger)
    : IRegisterTaskRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    private readonly ILogger<TaskRepository> _logger = logger;

    public async Task<int> RegisterTaskAsync(FlowTask task, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Registering a task.");

            var parameters = new
            {
                TaskId = task.Id,
                task.UserId,
                task.Title,
                task.Description,
                Status = task.Status.ToString(),
                CreatedAt = task.CreationDate,
                UpdatedAt = DateTime.UtcNow
            };

            var sql = @"
                INSERT INTO task (task_id, user_id, title, description, status, created_at, updated_at)
                VALUES (@TaskId, @UserId, @Title, @Description, @Status, @CreatedAt, @UpdatedAt)";

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.ExecuteAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering a task.");
            throw;
        }
    }
}
