namespace TaskFlowHub.UnitTests.Core.Application.UseCases.UserLogin;

public class LoginUserUseCaseTests
{
    private readonly IFixture _fixture;

    private readonly Mock<IValidator<UserLoginInbound>> _validator;

    private readonly Mock<IUserLoginRepository> _repository;

    private readonly Mock<IUserLoginTokenGenerator> _tokenGenerator;

    private readonly Mock<IUserLoginOutcomeHandler> _outcomeHandler;

    private readonly LoginUserUseCase _sut;

    public LoginUserUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _validator = _fixture.Freeze<Mock<IValidator<UserLoginInbound>>>();
        _repository = _fixture.Freeze<Mock<IUserLoginRepository>>();
        _tokenGenerator = _fixture.Freeze<Mock<IUserLoginTokenGenerator>>();
        _outcomeHandler = _fixture.Freeze<Mock<IUserLoginOutcomeHandler>>();

        _sut = new LoginUserUseCase(_validator.Object, _repository.Object, _tokenGenerator.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler 'InvalidData' Must Be Called When Inbound Data Is Invalid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the 'InvalidData' outcome handler is called when the inbound data is invalid.")]
    public async Task InvalidData_OutcomeHandlerMustBeCalledWhenInboundDataIsInvalid()
    {
        // Arrange
        var inbound = _fixture.Create<UserLoginInbound>();
        var validationResult = _fixture.Create<ValidationResult>();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _outcomeHandler
            .Setup(x => x.InvalidData(It.IsAny<IDictionary<string, string[]>>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.InvalidData(It.IsAny<IDictionary<string, string[]>>()), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler 'UserNotRegistered' Must Be Called When User Does Not Exist")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the 'UserNotRegistered' outcome handler is called when the user does not exist.")]
    public async Task UserNotRegistered_OutcomeHandlerMustBeCalledWhenUserDoesNotExist()
    {
        // Arrange
        var inbound = _fixture.Create<UserLoginInbound>();
        var validationResult = new ValidationResult();
        var user = (User?)null;

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.GetUserByUsernameAsync(inbound.Username, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _outcomeHandler.Setup(x => x.UserNotRegistered()).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.UserNotRegistered(), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler 'IncorrectPassword' Must Be Called When Password Is Incorrect")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the 'IncorrectPassword' outcome handler is called when the password is incorrect.")]
    public async Task IncorrectPassword_OutcomeHandlerMustBeCalledWhenPasswordIsIncorrect()
    {
        // Arrange
        var inbound = _fixture.Create<UserLoginInbound>();
        var validationResult = new ValidationResult();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword($"{inbound.Password}-wrong");
        var user = _fixture.Build<User>()
            .With(x => x.Password, hashedPassword)
            .Create();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.GetUserByUsernameAsync(inbound.Username, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _outcomeHandler.Setup(x => x.IncorrectPassword()).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.IncorrectPassword(), Times.Once);
    }

    [Fact(DisplayName = "UpdateLastLoginDateAsync Must Be Called When User Is Successfully Logged In")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the 'UpdateLastLoginDateAsync' method is called when the user is successfully logged in.")]
    public async Task UpdateLastLoginDateAsync_MustBeCalledWhenUserIsSuccessfullyLoggedIn()
    {
        // Arrange
        var inbound = _fixture.Create<UserLoginInbound>();
        var validationResult = new ValidationResult();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(inbound.Password);
        var user = _fixture.Build<User>()
            .With(x => x.Password, hashedPassword)
            .Create();
        var jwtToken = _fixture.Create<string>();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.GetUserByUsernameAsync(inbound.Username, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _tokenGenerator.Setup(x => x.GenerateToken(user)).Returns(jwtToken);

        _repository
            .Setup(x => x.UpdateLastLoginDateAsync(user.Id, It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _repository.Verify(x => x.UpdateLastLoginDateAsync(user.Id, It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler 'UserLoggedIn' Must Be Called When User Is Successfully Logged In")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the 'UserLoggedIn' outcome handler is called when the user is successfully logged in.")]
    public async Task UserLoggedIn_OutcomeHandlerMustBeCalledWhenUserIsSuccessfullyLoggedIn()
    {
        // Arrange
        var inbound = _fixture.Create<UserLoginInbound>();
        var validationResult = new ValidationResult();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(inbound.Password);
        var user = _fixture.Build<User>()
            .With(x => x.Password, hashedPassword)
            .Create();
        var jwtToken = _fixture.Create<string>();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.GetUserByUsernameAsync(inbound.Username, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _tokenGenerator.Setup(x => x.GenerateToken(user)).Returns(jwtToken);

        _outcomeHandler.Setup(x => x.UserLoggedIn(user.Id, jwtToken)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.UserLoggedIn(user.Id, jwtToken), Times.Once);
    }
}
