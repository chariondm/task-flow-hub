namespace TaskFlowHub.Infrastructure.Security;

public static class JwtTokenSetup
{
    /// <summary>
    /// Adds JWT token generation services to the specified services collection.
    /// </summary>
    /// <param name="services">The services collection to add the JWT token generation services to.</param>
    /// <param name="configuration">The configuration.</param>
    /// <remarks>
    /// This method adds JWT token generation services to the specified services collection.
    /// </remarks>
    public static void AddJwtTokenGeneration(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtTokenSettings = configuration.GetSection("JwtTokenSettings");
        services.Configure<JwtTokenSettings>(jwtTokenSettings!);

        services.AddSingleton<IUserLoginTokenGenerator, JwtTokenGenerator>();
    }
}
