namespace TaskFlowHub.Infrastructure.Security;

public static class JwtTokenSetup
{
    /// <summary>
    /// Adds JWT token generation services to the specified services collection.
    /// </summary>
    /// <param name="services">The services collection to add the JWT token generation services to.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The services collection with the JWT token generation services added.</returns>
    /// <remarks>
    /// This method adds JWT token generation services to the specified services collection.
    /// </remarks>
    public static IServiceCollection AddJwtTokenGeneration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtTokenSettings = configuration.GetSection("JwtTokenSettings");
        services.Configure<JwtTokenSettings>(jwtTokenSettings!);

        services.AddSingleton<IUserLoginTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    /// <summary>
    /// Adds JWT token authentication services to the specified services collection.
    /// </summary>
    /// <param name="services">The services collection to add the JWT token authentication services to.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The services collection with the JWT token authentication services added.</returns>
    /// <remarks>
    /// This method adds JWT token authentication services to the specified services collection.
    /// </remarks>
    public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtTokenSettings = configuration.GetSection("JwtTokenSettings");
        services.Configure<JwtTokenSettings>(jwtTokenSettings!);

        var jwtTokenSettingsValue = jwtTokenSettings.Get<JwtTokenSettings>();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSettingsValue!.Secret));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtTokenSettingsValue.Issuer,
                ValidAudience = jwtTokenSettingsValue.Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("Admin", policy => policy.RequireRole("admin"));

        return services;
    }
}
