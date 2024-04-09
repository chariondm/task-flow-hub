namespace TaskFlowHub.UnitTests.Core.Application.UseCases.Tasks.RegisterTasks;

public class RegisterTaskUseCaseTests
{
    private readonly IFixture _fixture;

    private readonly Mock<IValidator<RegisterTaskInbound>> _validator;

    private readonly Mock<IRegisterTaskRepository> _repository;

    private readonly Mock<IRegisterTaskOutcomeHandler> _outcomeHandler;

    private readonly RegisterTaskUseCase _sut;

    public RegisterTaskUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _validator = _fixture.Freeze<Mock<IValidator<RegisterTaskInbound>>>();
        _repository = _fixture.Freeze<Mock<IRegisterTaskRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IRegisterTaskOutcomeHandler>>();

        _sut = new RegisterTaskUseCase(_validator.Object, _repository.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler 'InvalidData' Must Be Called When Inbound Data Is Invalid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterTask")]
    [Trait("Description", "Ensure that the 'InvalidData' outcome handler is called when the inbound data is invalid.")]
    public async Task InvalidData_OutcomeHandlerMustBeCalledWhenInboundDataIsInvalid()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterTaskInbound>();
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

    [Fact(DisplayName = "Outcome Handler 'TaskRegistered' Must Be Called When Task Is Registered")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterTask")]
    [Trait("Description", "Ensure that the 'TaskRegistered' outcome handler is called when the task is registered.")]
    public async Task TaskRegistered_OutcomeHandlerMustBeCalledWhenTaskIsRegistered()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterTaskInbound>();
        var validationResult = new ValidationResult();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _outcomeHandler
            .Setup(x => x.TaskRegistered(It.IsAny<Guid>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.TaskRegistered(It.IsAny<Guid>()), Times.Once);
    }

    [Fact(DisplayName = "Task Must Be Registered When Inbound Data Is Valid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterTask")]
    [Trait("Description", "Ensure that the task is registered when the inbound data is valid.")]
    public async Task TaskMustBeRegisteredWhenInboundDataIsValid()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterTaskInbound>();
        var validationResult = new ValidationResult();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.RegisterTaskAsync(It.IsAny<FlowTask>(), It.IsAny<CancellationToken>()))
            .Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _repository.Verify(x => x.RegisterTaskAsync(It.IsAny<FlowTask>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
