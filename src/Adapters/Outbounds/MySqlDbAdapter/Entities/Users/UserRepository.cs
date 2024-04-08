namespace TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Entities.Users;

/// <summary>
/// Represents the user repository.
/// </summary>
/// <param name="dbConnectionFactory">The database connection factory.</param>
/// <param name="logger">The logger.</param>
/// <remarks>
/// This class is responsible for handling the user data in the mysql database.
/// </remarks>
public class UserRepository(IDbConnectionFactory dbConnectionFactory, ILogger<UserRepository> logger)
    : IRegisterNonAdminUserUseRepository, IUserLoginRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    private readonly ILogger<UserRepository> _logger = logger;

    public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Getting a user by username.");

            var parameters = new { Username = username };

            var sql = @"
                SELECT user_id AS Id, username AS Username, email AS Email, password AS Password, is_admin AS IsAdmin
                FROM user
                WHERE username = @Username";

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.QueryFirstOrDefaultAsync<User>(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting a user by username.");
            throw;
        }
    }

    public async Task<bool> IsUsernameOrEmailRegisteredAsync(string username, string email, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Checking if the username or email is already registered.");

            var parameters = new { Username = username, Email = email };

            var sql = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM user
                    WHERE username = @Username OR email = @Email
                )";

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.QueryFirstAsync<bool>(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while checking if the username or email is already registered.");
            throw;
        }
    }

    /// <summary>
    /// Asynchronously registers a new non-admin user in the database.
    /// </summary>
    /// <param name="user">The user to be registered. The user object should contain the username, email, and other relevant information.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the number of affected rows.
    /// Returns 1 if the user was successfully added. Returns 0 if a user with the same username or email already
    /// exists, indicating that the user has not been added to the database.
    /// </returns>
    /// <remarks>
    /// This method attempts to insert a new user record into the database. If an attempt is made to register a user
    /// with a username or email that already exists, the method catches the duplicate key exception and returns 0,
    /// thereby indicating no new user was added without throwing an exception. This approach is designed to gracefully
    /// handle cases of duplicate entries in a high-concurrency environment.
    /// </remarks>
    public async Task<int> RegisterNonAdminUserAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Registering a new user.");

            var parameters = new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Password,
                IsAdmin = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var sql = @"
                INSERT INTO user (user_id, username, email, password, is_admin, created_at, updated_at)
                VALUES (@Id, @Username, @Email, @Password, @IsAdmin, @CreatedAt, @UpdatedAt)";

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.ExecuteAsync(command);
        }
        catch (MySqlException ex) when (ex.Number == (int)MySqlErrorCode.DuplicateKeyEntry)
        {
            _logger.LogWarning("The user already exists in the database.");
            return 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering a new user.");
            throw;
        }
    }

    public async Task<int> UpdateLastLoginDateAsync(Guid userId, DateTime lastLoginDate, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Updating the last login date.");

            var parameters = new { UserId = userId, LastLoginDate = lastLoginDate };

            var sql = @"
                UPDATE user
                SET updated_at = @LastLoginDate
                WHERE user_id = @UserId";

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.ExecuteAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the last login date.");
            throw;
        }
    }
}
