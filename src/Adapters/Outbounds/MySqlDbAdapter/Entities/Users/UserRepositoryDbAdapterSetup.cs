namespace TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Entities.Users;

public static class UserRepositoryDbAdapterSetup
{
    public static void AddUserRepositoryDbAdapter(this IServiceCollection services)
    {
        services
            .AddScoped<IAdminListUsersRepository, UserRepository>()
            .AddScoped<IRegisterNonAdminUserUseRepository, UserRepository>()
            .AddScoped<IUserLoginRepository, UserRepository>();
    }
}
