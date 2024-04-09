using Bogus;

namespace TaskFlowHub.UnitTests.Core.Application.UseCases.Tasks.RetrieveTasks.Inbounds;

public class RetrieveTaskInboundValidatorTests
{
    private readonly Fixture _fixture;

    private readonly RetrieveTaskInboundValidator _sut;

    public RetrieveTaskInboundValidatorTests()
    {
        _fixture = new Fixture();
        _sut = new RetrieveTaskInboundValidator();
    }

    [Fact(DisplayName = "Task Id Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RetrieveTask")]
    [Trait("Description", "Ensure that the Task Id must not be empty.")]
    public void TaskId_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<RetrieveTaskInbound>()
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
    [Trait("UseCase", "RetrieveTask")]
    [Trait("Description", "Ensure that the User Id must not be empty.")]
    public void UserId_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<RetrieveTaskInbound>()
            .With(x => x.UserId, Guid.Empty)
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.UserId));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "User Id is required.");
    }

    [Fact(DisplayName = "Inbound Data Must Be Valid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RetrieveTask")]
    [Trait("Description", "Ensure that the inbound data must be valid.")]
    public void InboundData_MustBeValid()
    {
        // Arrange
        var inbound = new Faker<RetrieveTaskInbound>()
            .CustomInstantiator(faker => new RetrieveTaskInbound(
                faker.Random.Guid(),
                faker.Random.Guid(),
                faker.Random.Bool()));

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}
