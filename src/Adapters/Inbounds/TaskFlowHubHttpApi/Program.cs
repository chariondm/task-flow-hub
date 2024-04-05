using System.Text.Json.Serialization;

using TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Modules.Common.Swagger;

using TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Entities.Users;

using TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter.Infrastructure.ConnectionFactory;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder
    .Services
        .AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();

builder.Services
    .AddDbConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection")!)
    .AddUserRepositoryDbAdapter();

builder.Services
    .AddRegisterNonAdminUserUseCase();

var app = builder.Build();

app.UseCustomSwagger(app.Environment);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
