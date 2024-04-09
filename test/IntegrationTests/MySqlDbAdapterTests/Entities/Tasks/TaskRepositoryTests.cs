using System.Reflection;

using Bogus.Extensions;

namespace TaskFlowHub.IntegrationTests.MySqlDbAdapterTests.Entities.Tasks;

public class TaskRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly Faker _faker = new();

    private readonly CancellationTokenSource? _cts;

    private readonly UserRepository _userRepository;

    private readonly TaskRepository _sut;

    public TaskRepositoryTests(DatabaseFixture fixture)
    {
        var logger = LoggerFactory.Create(builder => builder
            .AddFilter("TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter", LogLevel.Debug)
            .AddConsole());

        _userRepository = new UserRepository(fixture.DbConnectionFactory!, logger.CreateLogger<UserRepository>());

        _sut = new TaskRepository(fixture.DbConnectionFactory!, logger.CreateLogger<TaskRepository>());

        _cts = fixture.Cts!;
    }

    [Fact(DisplayName = "RegisterTaskAsync Should Register Task Successfully")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "Task")]
    [Trait("Description", "Ensure that the 'RegisterTaskAsync' method registers a task successfully.")]
    public async Task RegisterTaskAsync_ShouldRegisterTaskSuccessfully()
    {
        // Arrange
        var user = new User(
            _faker.Random.Guid(),
            _faker.Internet.UserName(),
            _faker.Internet.Email(),
            _faker.Internet.Password(),
            _faker.Random.Bool());

        await _userRepository.RegisterUserAsync(user, _cts!.Token);

        var task = new FlowTask(
            _faker.Random.Guid(),
            user.Id,
            _faker.Lorem.Sentence().ClampLength(10, 100, '.'),
            _faker.Lorem.Paragraph().ClampLength(10, 1000, '.'),
            FlowTaskStatus.Created,
            DateTime.UtcNow);

        // Act
        var result = await _sut.RegisterTaskAsync(task, _cts!.Token);

        // Assert
        result.Should().Be(1);
    }

    [Fact(DisplayName = "RegisterTaskAsync Should Throw Exception When Task Registration Fails")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "Task")]
    [Trait("Description", "Ensure that the 'RegisterTaskAsync' method throws an exception when task registration fails.")]
    public async Task RegisterTaskAsync_ShouldThrowExceptionWhenTaskRegistrationFails()
    {
        // Arrange
        var task = new FlowTask(
            _faker.Random.Guid(),
            _faker.Random.Guid(),
            _faker.Lorem.Sentence().ClampLength(10, 100, '.'),
            _faker.Lorem.Paragraph().ClampLength(10, 1000, '.'),
            FlowTaskStatus.Created,
            DateTime.UtcNow);

        // Act
        Func<Task> act = async () => await _sut.RegisterTaskAsync(task, _cts!.Token);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
