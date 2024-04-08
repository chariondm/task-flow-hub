namespace TaskFlowHub.Core.Application.UseCases.Users.UserLogin;

public static class LoginUserUseCaseSetup
{
    public static IServiceCollection AddLoginUserUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<UserLoginInbound>, UserLoginInboundValidator>()
            .AddScoped<ILoginUserUseCase, LoginUserUseCase>();

        return services;
    }
}
