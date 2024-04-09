namespace TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Entities.Tasks;

public class TaskRepository(IDbConnectionFactory dbConnectionFactory, ILogger<TaskRepository> logger)
    : IListTaskRepository, IRetrieveTaskRepository, IRegisterTaskRepository, IUpdateTaskRepository
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

    public async Task<IEnumerable<FlowTask>> RetrieveAllTasksAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Retrieving all tasks.");

            var sql = @"
                SELECT task_id AS Id, user_id AS UserId, title AS Title, description AS Description, status AS Status, created_at AS CreationDate
                FROM task
                ORDER BY user_id, created_at DESC";

            var command = new CommandDefinition(sql, cancellationToken: cancellationToken);

            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.QueryAsync<FlowTask>(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all tasks.");
            throw;
        }
    }

    public async Task<FlowTask?> RetrieveTaskAsync(Guid taskId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Retrieving a task.");

            var parameters = new { TaskId = taskId };

            var sql = @"
                SELECT task_id AS Id, user_id AS UserId, title AS Title, description AS Description, status AS Status, created_at AS CreationDate
                FROM task
                WHERE task_id = @TaskId";

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.QueryFirstOrDefaultAsync<FlowTask>(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving a task.");
            throw;
        }
    }

    public async Task<IEnumerable<FlowTask>> RetrieveTasksAsync(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Retrieving tasks.");

            var parameters = new { UserId = userId };

            var sql = @"
                SELECT task_id AS Id, user_id AS UserId, title AS Title, description AS Description, status AS Status, created_at AS CreationDate
                FROM task
                WHERE user_id = @UserId
                ORDER BY created_at DESC";

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.QueryAsync<FlowTask>(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving tasks.");
            throw;
        }
    }

    public async Task<int> UpdateTaskAsync(FlowTask task, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Updating a task.");

            var parameters = new
            {
                TaskId = task.Id,
                task.UserId,
                task.Title,
                task.Description,
                Status = task.Status.ToString(),
                UpdatedAt = DateTime.UtcNow
            };

            var sql = @"
                UPDATE task
                SET title = @Title, description = @Description, status = @Status, updated_at = @UpdatedAt
                WHERE task_id = @TaskId AND user_id = @UserId";

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.ExecuteAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating a task.");
            throw;
        }
    }
}
