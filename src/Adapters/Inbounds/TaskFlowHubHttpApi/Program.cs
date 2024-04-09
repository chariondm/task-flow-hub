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
    .AddTaskRepositoryDbAdapter()
    .AddUserRepositoryDbAdapter();

builder.Services
    .AddJwtTokenGeneration(builder.Configuration)
    .AddJwtTokenAuthentication(builder.Configuration)
    .AddHttpContextAccessor()
    .AddScoped<IUserContextAccessor, UserContextAccessor>();

builder.Services
    .AddAdminListUsersUseCase()
    .AddRegisterNonAdminUserUseCase()
    .AddRegisterTaskUseCase()
    .AddLoginUserUseCase();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomSwagger(app.Environment);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
