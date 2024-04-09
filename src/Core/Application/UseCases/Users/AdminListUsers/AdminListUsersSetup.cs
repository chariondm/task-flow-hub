namespace TaskFlowHub.Core.Application.UseCases.Users.AdminListUsers;

public static class AdminListUsersSetup
{
    public static IServiceCollection AddAdminListUsersUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IAdminListUsersUseCase, AdminListUsersUseCase>();

        return services;
    }
}
