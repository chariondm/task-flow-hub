namespace TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Entities.Users;

public static class UserRepositoryDbAdapterSetup
{
    public static void AddUserRepositoryDbAdapter(this IServiceCollection services)
    {
        services.AddScoped<IRegisterNonAdminUserUseRepository, UserRepository>();
    }
}
