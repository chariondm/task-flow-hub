namespace TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Entities.Tasks;

public static class TaskRepositoryDbAdapterSetup
{
    public static IServiceCollection AddTaskRepositoryDbAdapter(this IServiceCollection services)
    {
        services
            .AddScoped<IListTaskRepository, TaskRepository>()
            .AddScoped<IRetrieveTaskRepository, TaskRepository>()
            .AddScoped<IRegisterTaskRepository, TaskRepository>()
            .AddScoped<IUpdateTaskRepository, TaskRepository>();

        return services;
    }
}
