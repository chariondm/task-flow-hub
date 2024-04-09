namespace TaskFlowHub.Core.Application.UseCases.Tasks.ListTasks;

public static class ListTaskSetup
{
    public static IServiceCollection AddListTaskUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<ListTaskInbound>, ListTaskInboundValidator>()
            .AddScoped<IListTaskUseCase, ListTaskUseCase>();

        return services;
    }
}
