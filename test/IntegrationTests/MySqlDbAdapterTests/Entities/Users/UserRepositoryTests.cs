using System.Reflection;

namespace TaskFlowHub.IntegrationTests.MySqlDbAdapterTests.Entities.Users;

public class UserRepositoryTest : IClassFixture<DatabaseFixture>
{
    private readonly Faker _faker = new();

    private readonly CancellationTokenSource? _cts;

    private readonly UserRepository _sut;

    public UserRepositoryTest(DatabaseFixture fixture)
    {
        var loggerFactory = LoggerFactory.Create(builder => builder
                .AddFilter("TaskFlowHub.Adapters.Outbounds.MySqlDbAdapter", LogLevel.Debug)
                .AddConsole());

        ILogger<UserRepository> logger = loggerFactory.CreateLogger<UserRepository>();

        _sut = new UserRepository(fixture.DbConnectionFactory!, logger);

        _cts = fixture._cts;
    }

    [Fact(DisplayName = "IsUsernameOrEmailRegisteredAsync Should Return False When Username Or Email Is Not Registered")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'IsUsernameOrEmailRegisteredAsync' method returns false when the username or email is not registered.")]
    public async Task IsUsernameOrEmailRegisteredAsync_ShouldReturnFalse_WhenUsernameOrEmailIsNotRegistered()
    {
        // Arrange
        var username = _faker.Internet.UserName();
        var email = _faker.Internet.Email();

        // Act
        var result = await _sut.IsUsernameOrEmailRegisteredAsync(username, email, _cts!.Token);

        // Assert
        result.Should().BeFalse();
    }

    [Fact(DisplayName = "IsUsernameOrEmailRegisteredAsync Should Return True When Username Is Registered")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'IsUsernameOrEmailRegisteredAsync' method returns true when the username is registered.")]
    public async Task IsUsernameOrEmailRegisteredAsync_ShouldReturnTrue_WhenUsernameIsRegistered()
    {
        // Arrange
        var user = new User(
            _faker.Random.Guid(),
            _faker.Internet.UserName(),
            _faker.Internet.Email(),
            _faker.Internet.Password());

        await _sut.RegisterNonAdminUserAsync(user, _cts!.Token);

        // Act
        var result = await _sut.IsUsernameOrEmailRegisteredAsync(user.Username, _faker.Internet.Email(), _cts!.Token);

        // Assert
        result.Should().BeTrue();
    }

    [Fact(DisplayName = "IsUsernameOrEmailRegisteredAsync Should Throw Exception When An Error Occurs")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'IsUsernameOrEmailRegisteredAsync' method throws an exception when an error occurs.")]
    public async Task IsUsernameOrEmailRegisteredAsync_ShouldThrowException_WhenAnErrorOccurs()
    {
        // Arrange
        _sut.GetType().GetField("_dbConnectionFactory", BindingFlags.NonPublic | BindingFlags.Instance)!
            .SetValue(_sut, null);

        // Act
        Func<Task> act = async () => await _sut.IsUsernameOrEmailRegisteredAsync(
            _faker.Internet.UserName(),
            _faker.Internet.Email(),
            _cts!.Token);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact(DisplayName = "RegisterNonAdminUserAsync Should Return 1 When User Is Successfully Registered")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'RegisterNonAdminUserAsync' method returns 1 when the user is successfully registered.")]
    public async Task RegisterNonAdminUserAsync_ShouldReturnOne_WhenUserIsSuccessfullyRegistered()
    {
        // Arrange
        var user = new User(
            _faker.Random.Guid(),
            _faker.Internet.UserName(),
            _faker.Internet.Email(),
            _faker.Internet.Password());

        // Act
        var result = await _sut.RegisterNonAdminUserAsync(user, _cts!.Token);

        // Assert
        result.Should().Be(1);
    }

    [Fact(DisplayName = "RegisterNonAdminUserAsync Should Return 0 When User With Same Username Is Already Registered")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'RegisterNonAdminUserAsync' method returns 0 when a user with the same username is already registered.")]
    public async Task RegisterNonAdminUserAsync_ShouldReturnZero_WhenUserWithSameUsernameIsAlreadyRegistered()
    {
        // Arrange
        var user = new User(
            _faker.Random.Guid(),
            _faker.Internet.UserName(),
            _faker.Internet.Email(),
            _faker.Internet.Password());

        await _sut.RegisterNonAdminUserAsync(user, _cts!.Token);

        // Act
        var result = await _sut.RegisterNonAdminUserAsync(user, _cts!.Token);

        // Assert
        result.Should().Be(0);
    }

    [Fact(DisplayName = "RegisterNonAdminUserAsync Should Throw Exception When An Error Occurs")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'RegisterNonAdminUserAsync' method throws an exception when an error occurs.")]
    public async Task RegisterNonAdminUserAsync_ShouldThrowException_WhenAnErrorOccurs()
    {
        // Arrange
        var user = new User(
            _faker.Random.Guid(),
            _faker.Internet.UserName(),
            _faker.Internet.Email(),
            _faker.Internet.Password());

        _sut.GetType().GetField("_dbConnectionFactory", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(_sut, null);

        // Act
        Func<Task> act = async () => await _sut.RegisterNonAdminUserAsync(user, _cts!.Token);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact(DisplayName = "GetUserByUsernameAsync Should Return User When User Is Found")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'GetUserByUsernameAsync' method returns the user when the user is found.")]
    public async Task GetUserByUsernameAsync_ShouldReturnUser_WhenUserIsFound()
    {
        // Arrange
        var user = new User(
            _faker.Random.Guid(),
            _faker.Internet.UserName(),
            _faker.Internet.Email(),
            _faker.Internet.Password());

        await _sut.RegisterNonAdminUserAsync(user, _cts!.Token);

        // Act
        var result = await _sut.GetUserByUsernameAsync(user.Username, _cts!.Token);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(user.Id);
        result.Username.Should().Be(user.Username);
        result.Email.Should().Be(user.Email);
        result.Password.Should().Be(user.Password);
        result.IsAdmin.Should().BeFalse();
    }

    [Fact(DisplayName = "GetUserByUsernameAsync Should Return Null When User Is Not Found")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'GetUserByUsernameAsync' method returns null when the user is not found.")]
    public async Task GetUserByUsernameAsync_ShouldReturnNull_WhenUserIsNotFound()
    {
        // Arrange
        var username = _faker.Internet.UserName();

        // Act
        var result = await _sut.GetUserByUsernameAsync(username, _cts!.Token);

        // Assert
        result.Should().BeNull();
    }

    [Fact(DisplayName = "GetUserByUsernameAsync Should Throw Exception When An Error Occurs")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'GetUserByUsernameAsync' method throws an exception when an error occurs.")]
    public async Task GetUserByUsernameAsync_ShouldThrowException_WhenAnErrorOccurs()
    {
        // Arrange
        _sut.GetType().GetField("_dbConnectionFactory", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(_sut, null);

        // Act
        Func<Task> act = async () => await _sut.GetUserByUsernameAsync(_faker.Internet.UserName(), _cts!.Token);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact(DisplayName = "UpdateLastLoginDateAsync Should Update Last User Login Date")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'UpdateLastLoginDateAsync' method updates the last user login date.")]
    public async Task UpdateLastLoginDateAsync_ShouldUpdateLastUserLoginDate()
    {
        // Arrange
        var user = new User(
            _faker.Random.Guid(),
            _faker.Internet.UserName(),
            _faker.Internet.Email(),
            _faker.Internet.Password());

        await _sut.RegisterNonAdminUserAsync(user, _cts!.Token);

        var lastLoginDate = DateTime.UtcNow;

        // Act
        var result = await _sut.UpdateLastLoginDateAsync(user.Id, lastLoginDate, _cts!.Token);

        // Assert
        result.Should().Be(1);
    }

    [Fact(DisplayName = "UpdateLastLoginDateAsync Should Throw Exception When An Error Occurs")]
    [Trait("Category", "Integration Test")]
    [Trait("Entity", "User")]
    [Trait("Description", "Ensure that the 'UpdateLastLoginDateAsync' method throws an exception when an error occurs.")]
    public async Task UpdateLastLoginDateAsync_ShouldThrowException_WhenAnErrorOccurs()
    {
        // Arrange
        _sut.GetType().GetField("_dbConnectionFactory", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(_sut, null);

        // Act
        Func<Task> act = async () => await _sut.UpdateLastLoginDateAsync(_faker.Random.Guid(), DateTime.UtcNow, _cts!.Token);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
