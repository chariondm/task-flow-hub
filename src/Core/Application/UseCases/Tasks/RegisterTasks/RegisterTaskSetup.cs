namespace TaskFlowHub.Core.Application.UseCases.Tasks.RegisterTasks;

public static class RegisterTaskSetup
{
    public static IServiceCollection AddRegisterTaskUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<RegisterTaskInbound>, RegisterTaskInboundValidator>()
            .AddScoped<IRegisterTaskUseCase, RegisterTaskUseCase>();

        return services;
    }
}
