namespace TaskFlowHub.UnitTests.Core.Application.UseCases.RegisterNonAdminUser;

public class RegisterNonAdminUserUseCaseTests
{
    private readonly IFixture _fixture;

    private readonly Mock<IValidator<RegisterNonAdminUserInbound>> _validator;

    private readonly Mock<IRegisterNonAdminUserUseRepository> _repository;

    private readonly Mock<IRegisterNonAdminUserOutcomeHandler> _outcomeHandler;

    private readonly RegisterNonAdminUserUseCase _sut;

    public RegisterNonAdminUserUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _validator = _fixture.Freeze<Mock<IValidator<RegisterNonAdminUserInbound>>>();
        _repository = _fixture.Freeze<Mock<IRegisterNonAdminUserUseRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IRegisterNonAdminUserOutcomeHandler>>();

        _sut = new RegisterNonAdminUserUseCase(_validator.Object, _repository.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler 'InvalidData' Must Be Called When Inbound Data Is Invalid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterNonAdminUser")]
    [Trait("Description", "Ensure that the 'InvalidData' outcome handler is called when the inbound data is invalid.")]
    public async Task InvalidData_OutcomeHandlerMustBeCalledWhenInboundDataIsInvalid()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterNonAdminUserInbound>();
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

    [Fact(DisplayName = "Outcome Handler 'UserAlreadyRegistered' Must Be Called When User Already Exists")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterNonAdminUser")]
    [Trait("Description", "Ensure that the 'UserAlreadyRegistered' outcome handler is called when the user already exists.")]
    public async Task UserAlreadyRegistered_OutcomeHandlerMustBeCalledWhenUserAlreadyExists()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterNonAdminUserInbound>();
        var validationResult = new ValidationResult();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.IsUsernameOrEmailRegisteredAsync(inbound.Username, inbound.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _outcomeHandler
            .Setup(x => x.UserAlreadyRegistered()).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.UserAlreadyRegistered(), Times.Once);
    }

    // Call UserAlreadyRegistered when race condition occurs
    [Fact(DisplayName = "Outcome Handler 'UserAlreadyRegistered' Must Be Called When Race Condition Occurs")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterNonAdminUser")]
    [Trait("Description", "Ensure that the 'UserAlreadyRegistered' outcome handler is called when race condition occurs.")]
    public async Task UserAlreadyRegistered_OutcomeHandlerMustBeCalledWhenRaceConditionOccurs()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterNonAdminUserInbound>();
        var validationResult = new ValidationResult();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.IsUsernameOrEmailRegisteredAsync(inbound.Username, inbound.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _repository
            .Setup(x => x.RegisterNonAdminUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        _outcomeHandler
            .Setup(x => x.UserAlreadyRegistered()).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.UserAlreadyRegistered(), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler 'UserRegistered' Must Be Called When User Is Registered")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterNonAdminUser")]
    [Trait("Description", "Ensure that the 'UserRegistered' outcome handler is called when the user is registered.")]
    public async Task UserRegistered_OutcomeHandlerMustBeCalledWhenUserIsRegistered()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterNonAdminUserInbound>();
        var validationResult = new ValidationResult();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.IsUsernameOrEmailRegisteredAsync(inbound.Username, inbound.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _repository
            .Setup(x => x.RegisterNonAdminUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _outcomeHandler
            .Setup(x => x.NonAdminUserRegistered(It.IsAny<Guid>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.NonAdminUserRegistered(It.IsAny<Guid>()), Times.Once);
    }
}