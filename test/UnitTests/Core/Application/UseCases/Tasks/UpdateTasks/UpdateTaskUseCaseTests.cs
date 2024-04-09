namespace TaskFlowHub.UnitTests.Core.Application.UseCases.Tasks.UpdateTasks;

public class UpdateTaskUseCaseTests
{
    private readonly IFixture _fixture;

    private readonly Mock<IValidator<UpdateTaskInbound>> _validator;

    private readonly Mock<IUpdateTaskRepository> _repository;

    private readonly Mock<IUpdateTaskOutcomeHandler> _outcomeHandler;

    private readonly UpdateTaskUseCase _sut;

    public UpdateTaskUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _validator = _fixture.Freeze<Mock<IValidator<UpdateTaskInbound>>>();
        _repository = _fixture.Freeze<Mock<IUpdateTaskRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IUpdateTaskOutcomeHandler>>();

        _sut = new UpdateTaskUseCase(_validator.Object, _repository.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler 'InvalidData' Must Be Called When Inbound Data Is Invalid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the 'InvalidData' outcome handler is called when the inbound data is invalid.")]
    public async Task InvalidData_OutcomeHandlerMustBeCalledWhenInboundDataIsInvalid()
    {
        // Arrange
        var inbound = _fixture.Create<UpdateTaskInbound>();
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

    [Fact(DisplayName = "Outcome Handler 'TaskNotFound' Must Be Called When Task Is Not Found")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the 'TaskNotFound' outcome handler is called when the task is not found.")]
    public async Task TaskNotFound_OutcomeHandlerMustBeCalledWhenTaskIsNotFound()
    {
        // Arrange
        var inbound = _fixture.Create<UpdateTaskInbound>();
        var validationResult = new ValidationResult();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.UpdateTaskAsync(It.IsAny<FlowTask>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        _outcomeHandler
            .Setup(x => x.TaskNotFound()).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.TaskNotFound(), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler 'TaskUpdated' Must Be Called When Task Is Updated")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the 'TaskUpdated' outcome handler is called when the task is updated.")]
    public async Task TaskUpdated_OutcomeHandlerMustBeCalledWhenTaskIsUpdated()
    {
        // Arrange
        var inbound = _fixture.Create<UpdateTaskInbound>();
        var validationResult = new ValidationResult();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.UpdateTaskAsync(It.IsAny<FlowTask>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _outcomeHandler
            .Setup(x => x.TaskUpdated()).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.TaskUpdated(), Times.Once);
    }

    [Fact(DisplayName = "Repository 'UpdateTaskAsync' Must Be Called With Task When Inbound Data Is Valid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the 'UpdateTaskAsync' repository method is called with the task when the inbound data is valid.")]
    public async Task UpdateTaskAsync_RepositoryMustBeCalledWithTaskWhenInboundDataIsValid()
    {
        // Arrange
        var inbound = _fixture.Create<UpdateTaskInbound>();
        var validationResult = new ValidationResult();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _repository
            .Setup(x => x.UpdateTaskAsync(It.IsAny<FlowTask>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _repository.Verify(x => x.UpdateTaskAsync(It.IsAny<FlowTask>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
