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
        User user = await CreateAndRegisterUserAsync();

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
        var task = await CreateAndRegisterTaskAsync(await CreateAndRegisterUserAsync());

        // Act
        Func<Task> act = async () => await _sut.RegisterTaskAsync(task, _cts!.Token);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact(DisplayName = "RetrieveAllTasksAsync Should Retrieve All Tasks Successfully")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "Task")]
    [Trait("Description", "Ensure that the 'RetrieveAllTasksAsync' method retrieves all tasks successfully.")]
    public async Task RetrieveAllTasksAsync_ShouldRetrieveAllTasksSuccessfully()
    {
        // Arrange
        var user = await CreateAndRegisterUserAsync();
        await CreateAndRegisterTaskAsync(user);

        // Act
        var result = await _sut.RetrieveAllTasksAsync(_cts!.Token);

        // Assert
        result.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "RetrieveAllTasksAsync Should Throw Exception When Task Retrieval Fails")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "Task")]
    [Trait("Description", "Ensure that the 'RetrieveAllTasksAsync' method throws an exception when task retrieval fails.")]
    public async Task RetrieveAllTasksAsync_ShouldThrowExceptionWhenTaskRetrievalFails()
    {
        // Arrange
        _sut.GetType().GetField("_dbConnectionFactory", BindingFlags.NonPublic | BindingFlags.Instance)!
            .SetValue(_sut, null);

        // Act
        Func<Task> act = async () => await _sut.RetrieveAllTasksAsync(_cts!.Token);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact(DisplayName = "RetrieveTaskAsync Should Retrieve Task Successfully")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "Task")]
    [Trait("Description", "Ensure that the 'RetrieveTaskAsync' method retrieves a task successfully.")]
    public async Task RetrieveTaskAsync_ShouldRetrieveTaskSuccessfully()
    {
        // Arrange
        var user = await CreateAndRegisterUserAsync();

        var task = new FlowTask(
            _faker.Random.Guid(),
            user.Id,
            _faker.Lorem.Sentence().ClampLength(10, 100, '.'),
            _faker.Lorem.Paragraph().ClampLength(10, 1000, '.'),
            FlowTaskStatus.Created,
            DateTime.UtcNow);

        await _sut.RegisterTaskAsync(task, _cts!.Token);

        // Act
        var result = await _sut.RetrieveTaskAsync(task.Id, _cts!.Token);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "RetrieveTaskAsync Should Return Null When Task Is Not Found")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "Task")]
    [Trait("Description", "Ensure that the 'RetrieveTaskAsync' method returns null when the task is not found.")]
    public async Task RetrieveTaskAsync_ShouldReturnNullWhenTaskIsNotFound()
    {
        // Arrange
        var taskId = _faker.Random.Guid();

        // Act
        var result = await _sut.RetrieveTaskAsync(taskId, _cts!.Token);

        // Assert
        result.Should().BeNull();
    }

    [Fact(DisplayName = "RetrieveTaskAsync Should Throw Exception When Task Retrieval Fails")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "Task")]
    [Trait("Description", "Ensure that the 'RetrieveTaskAsync' method throws an exception when task retrieval fails.")]
    public async Task RetrieveTaskAsync_ShouldThrowExceptionWhenTaskRetrievalFails()
    {
        // Arrange
        var taskId = _faker.Random.Guid();

        _sut.GetType().GetField("_dbConnectionFactory", BindingFlags.NonPublic | BindingFlags.Instance)!
            .SetValue(_sut, null);

        // Act
        Func<Task> act = async () => await _sut.RetrieveTaskAsync(taskId, _cts!.Token);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact(DisplayName = "RetrieveTasksAsync Should Retrieve Tasks Successfully")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "Task")]
    [Trait("Description", "Ensure that the 'RetrieveTasksAsync' method retrieves tasks successfully.")]
    public async Task RetrieveTasksAsync_ShouldRetrieveTasksSuccessfully()
    {
        // Arrange
        var user = await CreateAndRegisterUserAsync();
        await CreateAndRegisterTaskAsync(user);

        // Act
        var result = await _sut.RetrieveTasksAsync(user.Id, _cts!.Token);

        // Assert
        result.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "RetrieveTasksAsync Should Throw Exception When Task Retrieval Fails")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "Task")]
    [Trait("Description", "Ensure that the 'RetrieveTasksAsync' method throws an exception when task retrieval fails.")]
    public async Task RetrieveTasksAsync_ShouldThrowExceptionWhenTaskRetrievalFails()
    {
        // Arrange
        var userId = _faker.Random.Guid();

        _sut.GetType().GetField("_dbConnectionFactory", BindingFlags.NonPublic | BindingFlags.Instance)!
            .SetValue(_sut, null);

        // Act
        Func<Task> act = async () => await _sut.RetrieveTasksAsync(userId, _cts!.Token);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    private async Task<User> CreateAndRegisterUserAsync()
    {
        // Arrange
        var user = new User(
            _faker.Random.Guid(),
            _faker.Internet.UserName(),
            _faker.Internet.Email(),
            _faker.Internet.Password(),
            _faker.Random.Bool());

        await _userRepository.RegisterUserAsync(user, _cts!.Token);

        return user;
    }

    private async Task<FlowTask> CreateAndRegisterTaskAsync(User user)
    {
        var task = new FlowTask(
            _faker.Random.Guid(),
            user.Id,
            _faker.Lorem.Sentence().ClampLength(10, 100, '.'),
            _faker.Lorem.Paragraph().ClampLength(10, 1000, '.'),
            FlowTaskStatus.Created,
            DateTime.UtcNow);

        await _sut.RegisterTaskAsync(task, _cts!.Token);

        return task;
    }
}
