namespace TaskFlowHub.UnitTests.Core.Application.UseCases.Tasks.RetrieveTasks;

public class RetrieveTaskUseCaseTests
{
    private readonly IFixture _fixture;

    private readonly Mock<IValidator<RetrieveTaskInbound>> _validator;

    private readonly Mock<IRetrieveTaskRepository> _repository;

    private readonly Mock<IRetrieveTaskOutcomeHandler> _outcomeHandler;

    private readonly RetrieveTaskUseCase _sut;

    public RetrieveTaskUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _validator = _fixture.Freeze<Mock<IValidator<RetrieveTaskInbound>>>();
        _repository = _fixture.Freeze<Mock<IRetrieveTaskRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IRetrieveTaskOutcomeHandler>>();

        _sut = new RetrieveTaskUseCase(_validator.Object, _repository.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler 'InvalidData' Must Be Called When Inbound Data Is Invalid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RetrieveTask")]
    [Trait("Description", "Ensure that the 'InvalidData' outcome handler is called when the inbound data is invalid.")]
    public async Task InvalidData_OutcomeHandlerMustBeCalledWhenInboundDataIsInvalid()
    {
        // Arrange
        var inbound = _fixture.Create<RetrieveTaskInbound>();
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

    [Fact(DisplayName = "Outcome Handler 'TaskNotFound' Must Be Called When Task Does Not Exist")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RetrieveTask")]
    [Trait("Description", "Ensure that the 'TaskNotFound' outcome handler is called when the task does not exist.")]
    public async Task TaskNotFound_OutcomeHandlerMustBeCalledWhenTaskDoesNotExist()
    {
        // Arrange
        var inbound = _fixture.Create<RetrieveTaskInbound>();
        var task = (FlowTask?)null;

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _repository
            .Setup(x => x.RetrieveTaskAsync(inbound.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);

        _outcomeHandler
            .Setup(x => x.TaskNotFound()).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.TaskNotFound(), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler 'AccessDenied' Must Be Called When User Cannot Access Task")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RetrieveTask")]
    [Trait("Description", "Ensure that the 'AccessDenied' outcome handler is called when the user cannot access the task.")]
    public async Task AccessDenied_OutcomeHandlerMustBeCalledWhenUserCannotAccessTask()
    {
        // Arrange
        var inbound = _fixture.Build<RetrieveTaskInbound>()
            .With(x => x.IsAdmin, false)
            .Create();
        var task = _fixture.Create<FlowTask>();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _repository
            .Setup(x => x.RetrieveTaskAsync(inbound.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);

        _outcomeHandler
            .Setup(x => x.AccessDenied()).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.AccessDenied(), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler 'TaskRetrieved' Must Be Called When Task Is Retrieved")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RetrieveTask")]
    [Trait("Description", "Ensure that the 'TaskRetrieved' outcome handler is called when the task is retrieved.")]
    public async Task TaskRetrieved_OutcomeHandlerMustBeCalledWhenTaskIsRetrieved()
    {
        // Arrange
        var inbound = _fixture.Create<RetrieveTaskInbound>();
        var task = _fixture.Build<FlowTask>()
            .With(x => x.UserId, inbound.UserId)
            .Create();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _repository
            .Setup(x => x.RetrieveTaskAsync(inbound.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);

        _outcomeHandler
            .Setup(x => x.TaskRetrieved(It.IsAny<FlowTask>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.TaskRetrieved(It.IsAny<FlowTask>()), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler 'TaskRetrieved' Must Be Called When Task Is Retrieved By Admin")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RetrieveTask")]
    [Trait("Description", "Ensure that the 'TaskRetrieved' outcome handler is called when the task is retrieved by an admin.")]
    public async Task TaskRetrieved_OutcomeHandlerMustBeCalledWhenTaskIsRetrievedByAdmin()
    {
        // Arrange
        var inbound = _fixture.Build<RetrieveTaskInbound>()
            .With(x => x.IsAdmin, true)
            .Create();
        var task = _fixture.Create<FlowTask>();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _repository
            .Setup(x => x.RetrieveTaskAsync(inbound.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);

        _outcomeHandler
            .Setup(x => x.TaskRetrieved(It.IsAny<FlowTask>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.TaskRetrieved(It.IsAny<FlowTask>()), Times.Once);
    }
}
