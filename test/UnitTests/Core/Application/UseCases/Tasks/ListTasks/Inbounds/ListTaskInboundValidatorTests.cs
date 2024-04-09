using Bogus;

namespace TaskFlowHub.UnitTests.Core.Application.UseCases.Tasks.ListTasks.Inbounds;

public class ListTaskInboundValidatorTests
{
    private readonly Fixture _fixture;

    private readonly ListTaskInboundValidator _sut;

    public ListTaskInboundValidatorTests()
    {
        _fixture = new Fixture();
        _sut = new ListTaskInboundValidator();
    }

    [Fact(DisplayName = "User Id Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "ListTask")]
    [Trait("Description", "Ensure that the User Id must not be empty.")]
    public void UserId_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<ListTaskInbound>()
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
    [Trait("UseCase", "ListTask")]
    [Trait("Description", "Ensure that the inbound data must be valid.")]
    public void InboundData_MustBeValid()
    {
        // Arrange
        var inbound = new Faker<ListTaskInbound>()
            .CustomInstantiator(faker => new ListTaskInbound(faker.Random.Guid(), faker.Random.Bool()));

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}
