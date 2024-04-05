namespace TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Infrastructure.ConnectionFactory;

public static class DbConnectionFactorySetup
{
    public static IServiceCollection AddDbConnectionFactory(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(new DbConnectionFactory(connectionString));
        return services;
    }
}
