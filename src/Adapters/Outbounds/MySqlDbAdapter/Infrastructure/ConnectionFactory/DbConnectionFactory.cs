namespace TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Infrastructure.ConnectionFactory;

/// <summary>
/// Represents the database connection factory.
/// </summary>
/// <remarks>
/// This class is used to create a new asynchronous database connection to the MySQL database.
/// </remarks>
/// <seealso cref="IDbConnectionFactory" />
/// <seealso cref="IDbConnection" />
/// <seealso cref="MySqlConnection" />
public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken)
    {
        var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }

}