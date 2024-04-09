namespace TaskFlowHub.Core.Application.UseCases.Tasks.RetrieveTasks;

public static class RetrieveTaskSetup
{
    public static IServiceCollection AddRetrieveTaskUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<RetrieveTaskInbound>, RetrieveTaskInboundValidator>()
            .AddScoped<IRetrieveTaskUseCase, RetrieveTaskUseCase>();

        return services;
    }
}
