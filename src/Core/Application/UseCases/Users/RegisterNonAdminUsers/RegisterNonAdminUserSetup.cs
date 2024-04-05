namespace TaskFlowHub.Core.Application.UseCases.Users.RegisterNonAdminUser;

public static class RegisterNonAdminUserSetup
{
    public static IServiceCollection AddRegisterNonAdminUserUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<RegisterNonAdminUserInbound>, RegisterNonAdminUserInboundValidator>()
            .AddScoped<IRegisterNonAdminUserUseCase, RegisterNonAdminUserUseCase>();

        return services;
    }
}
