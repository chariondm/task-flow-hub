namespace TaskFlowHub.Core.Application.UseCases.Tasks.UpdateTasks;

public static class UpdateTaskSetup
{
    public static IServiceCollection AddUpdateTaskUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<UpdateTaskInbound>, UpdateTaskInboundValidator>()
            .AddScoped<IUpdateTaskUseCase, UpdateTaskUseCase>();

        return services;
    }
}
