namespace TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Entities.Tasks;

public static class TaskRepositoryDbAdapterSetup
{
    public static IServiceCollection AddTaskRepositoryDbAdapter(this IServiceCollection services)
    {
        services.AddScoped<IRegisterTaskRepository, TaskRepository>();

        return services;
    }
}
