using Microsoft.Extensions.Logging;

using TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Infrastructure.ConnectionFactory;

namespace TaskFlowHub.IntegrationTests.MySqlDbAdapterTests.Helpers;

public class DatabaseFixture : IAsyncLifetime
{
    public AppDbContext? DbContext { get; private set; }
    public IDbConnectionFactory? DbConnectionFactory { get; private set; }
    public readonly CancellationTokenSource _cts = new();

    public async Task InitializeAsync()
    {
        var databaseName = $"task_flow_hub_db_test_{Guid.NewGuid():N}";
        var connectionString = $"Host=localhost;Port=3306;Database={databaseName};User Id=root;password=changeme_root;";
        var serverVersion = new MySqlServerVersion(new Version(8, 3));

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString, serverVersion)
            .EnableDetailedErrors()
            .LogTo(Console.WriteLine, LogLevel.Information)
            .Options;

        DbContext = new AppDbContext(options);
        DbConnectionFactory = new DbConnectionFactory(connectionString);

        await DbContext.Database.EnsureCreatedAsync(_cts.Token);
    }

    public async Task DisposeAsync()
    {
        await DbContext!.Database.EnsureDeletedAsync(_cts.Token);
        await DbContext.DisposeAsync();
    }
}
