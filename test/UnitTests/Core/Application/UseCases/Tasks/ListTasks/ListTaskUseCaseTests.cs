namespace TaskFlowHub.UnitTests.Core.Application.UseCases.Tasks.ListTasks;

public class ListTaskUseCaseTests
{
    private readonly IFixture _fixture;

    private readonly Mock<IValidator<ListTaskInbound>> _validator;

    private readonly Mock<IListTaskRepository> _repository;

    private readonly Mock<IListTaskOutcomeHandler> _outcomeHandler;

    private readonly ListTaskUseCase _sut;

    public ListTaskUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _validator = _fixture.Freeze<Mock<IValidator<ListTaskInbound>>>();
        _repository = _fixture.Freeze<Mock<IListTaskRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IListTaskOutcomeHandler>>();

        _sut = new ListTaskUseCase(_validator.Object, _repository.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler 'InvalidData' Must Be Called When Inbound Data Is Invalid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "ListTask")]
    [Trait("Description", "Ensure that the 'InvalidData' outcome handler is called when the inbound data is invalid.")]
    public async Task InvalidData_OutcomeHandlerMustBeCalledWhenInboundDataIsInvalid()
    {
        // Arrange
        var inbound = _fixture.Create<ListTaskInbound>();
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

    [Fact(DisplayName = "Outcome Handler 'TasksListed' Must Be Called When User's Tasks Are Retrieved")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "ListTask")]
    [Trait("Description", "Ensure that the 'TasksListed' outcome handler is called when the user's tasks are retrieved.")]
    public async Task TasksListed_OutcomeHandlerMustBeCalledWhenTaskAreRetrieved()
    {
        // Arrange
        var inbound = _fixture.Create<ListTaskInbound>();
        var tasks = _fixture.CreateMany<FlowTask>().ToList();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _repository
            .Setup(x => x.RetrieveTasksAsync(inbound.UserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tasks);

        _outcomeHandler
            .Setup(x => x.TasksListed(It.IsAny<IEnumerable<FlowTask>>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.TasksListed(It.IsAny<IEnumerable<FlowTask>>()), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler 'TasksListed' Must Be Called When All Tasks Are Retrieved")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "ListTask")]
    [Trait("Description", "Ensure that the 'TaskRetrieved' outcome handler is called when all tasks are retrieved.")]
    public async Task TaskRetrieved_OutcomeHandlerMustBeCalledWhenAllTasksAreRetrieved()
    {
        // Arrange
        var inbound = _fixture.Build<ListTaskInbound>()
            .With(x => x.IsAdmin, true)
            .Create();
        var tasks = _fixture.CreateMany<FlowTask>().ToList();

        _validator
            .Setup(x => x.ValidateAsync(inbound, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _repository
            .Setup(x => x.RetrieveAllTasksAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(tasks);

        _outcomeHandler
            .Setup(x => x.TasksListed(It.IsAny<IEnumerable<FlowTask>>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.TasksListed(It.IsAny<IEnumerable<FlowTask>>()), Times.Once);
    }
}
