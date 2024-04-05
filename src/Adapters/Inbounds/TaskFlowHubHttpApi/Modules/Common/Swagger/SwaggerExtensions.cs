namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Modules.Common.Swagger;

/// <summary>
/// Provides extension methods for configuring Swagger documentation and UI in an ASP.NET Core application.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Adds custom Swagger generation with predefined options to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The original IServiceCollection for chaining.</returns>
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "TaskFlowHub API",
                Description = "This ASP.NET Core Web API is designed to manage user, task, and workflow data for TaskFlowHub.",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Support Team",
                    Email = "support@example.com",
                    Url = new Uri("https://github.com/chariondm/task-flow-hub/issues")
                },
                License = new OpenApiLicense
                {
                    Name = "Use of this API is subject to the terms and conditions of the MIT License.",
                    Url = new Uri("https://github.com/chariondm/task-flow-hub?tab=MIT-1-ov-file")
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    /// <summary>
    /// Configures the application to use Swagger middleware for generating Swagger documentation and Swagger UI based on the environment.
    /// </summary>
    /// <param name="app">The IApplicationBuilder to configure.</param>
    /// <param name="env">The web hosting environment to check for development mode.</param>
    /// <returns>The original IApplicationBuilder for chaining.</returns>
    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskFlowHub API v1"));
        }

        return app;
    }
}
