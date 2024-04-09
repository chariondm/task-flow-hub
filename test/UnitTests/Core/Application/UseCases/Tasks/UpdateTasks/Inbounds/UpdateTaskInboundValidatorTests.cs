using Bogus;
using Bogus.Extensions;

namespace TaskFlowHub.UnitTests.Core.Application.UseCases.Tasks.UpdateTasks.Inbounds;

public class UpdateTaskInboundValidatorTests
{
    private readonly Fixture _fixture;

    private readonly UpdateTaskInboundValidator _sut;

    public UpdateTaskInboundValidatorTests()
    {
        _fixture = new Fixture();
        _sut = new UpdateTaskInboundValidator();
    }

    [Fact(DisplayName = "Task Id Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the Task Id must not be empty.")]
    public void TaskId_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<UpdateTaskInbound>()
            .With(x => x.Id, Guid.Empty)
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.Id));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Task Id is required.");
    }

    [Fact(DisplayName = "User Id Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the User Id must not be empty.")]
    public void UserId_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<UpdateTaskInbound>()
            .With(x => x.UserId, Guid.Empty)
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.UserId));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "User Id is required.");
    }

    [Fact(DisplayName = "Task Title Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the Task Title must not be empty.")]
    public void TaskTitle_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<UpdateTaskInbound>()
            .With(x => x.Title, string.Empty)
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == nameof(inbound.Title));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Task Title is required.");
    }

    [Fact(DisplayName = "Task Title Must Be At Least 10 Characters Long")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the Task Title must be at least 10 characters long.")]
    public void TaskTitle_MustBeAtLeast10CharactersLong()
    {
        // Arrange
        var inbound = _fixture.Build<UpdateTaskInbound>()
            .With(x => x.Title, "Task")
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.Title));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Task Title must be at least 10 characters.");
    }

    [Fact(DisplayName = "Task Title Must Not Exceed 100 Characters")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the Task Title must not exceed 100 characters.")]
    public void TaskTitle_MustNotExceed100Characters()
    {
        // Arrange
        var inbound = _fixture.Build<UpdateTaskInbound>()
            .With(x => x.Title, new string('A', 101))
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.Title));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Task Title must not exceed 100 characters.");
    }

    [Fact(DisplayName = "Task Description Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the Task Description must not be empty.")]
    public void TaskDescription_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<UpdateTaskInbound>()
            .With(x => x.Description, string.Empty)
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == nameof(inbound.Description));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Task Description is required.");
    }

    [Fact(DisplayName = "Task Description Must Be At Least 10 Characters Long")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the Task Description must be at least 10 characters long.")]
    public void TaskDescription_MustBeAtLeast10CharactersLong()
    {
        // Arrange
        var inbound = _fixture.Build<UpdateTaskInbound>()
            .With(x => x.Description, "Task")
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.Description));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Task Description must be at least 10 characters.");
    }

    [Fact(DisplayName = "Task Description Must Not Exceed 1000 Characters")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the Task Description must not exceed 1000 characters.")]
    public void TaskDescription_MustNotExceed1000Characters()
    {
        // Arrange
        var inbound = _fixture.Build<UpdateTaskInbound>()
            .With(x => x.Description, new string('A', 1001))
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.Description));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Task Description must not exceed 1000 characters.");
    }

    [Fact(DisplayName = "Task Must Be Valid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateTask")]
    [Trait("Description", "Ensure that the Task must be valid.")]
    public void Task_MustBeValid()
    {
        // Arrange
        var inbound = new Faker<UpdateTaskInbound>()
            .CustomInstantiator(faker => new UpdateTaskInbound(
                faker.Random.Guid(),
                faker.Random.Guid(),
                faker.Lorem.Sentence().ClampLength(10, 100),
                faker.Lorem.Sentence().ClampLength(10, 1000),
                faker.PickRandom<FlowTaskStatus>()));

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}
