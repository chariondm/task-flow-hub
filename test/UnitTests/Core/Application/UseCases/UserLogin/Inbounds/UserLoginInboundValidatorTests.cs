using Bogus;

namespace TaskFlowHub.UnitTests.Core.Application.UseCases.UserLogin.Inbounds;

public class UserLoginInboundValidatorTests
{
    private readonly Fixture _fixture;

    private readonly UserLoginInboundValidator _sut;

    public UserLoginInboundValidatorTests()
    {
        _fixture = new Fixture();
        _sut = new UserLoginInboundValidator();
    }

    [Fact(DisplayName = "Username Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the Username must not be empty.")]
    public void Username_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<UserLoginInbound>()
            .With(x => x.Username, string.Empty)
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == nameof(inbound.Username));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Username is required.");
    }

    [Fact(DisplayName = "Username Must Be At Least 3 Characters Long")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the Username must be at least 3 characters long.")]
    public void Username_MustBeAtLeast3CharactersLong()
    {
        // Arrange
        var inbound = _fixture.Build<UserLoginInbound>()
            .With(x => x.Username, "ab")
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.Username));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Username must be at least 3 characters long.");
    }

    [Fact(DisplayName = "Username Must Not Exceed 50 Characters")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the Username must not exceed 50 characters.")]
    public void Username_MustNotExceed50Characters()
    {
        // Arrange
        var inbound = _fixture.Build<UserLoginInbound>()
            .With(x => x.Username, new string('a', 51))
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.Username));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Username must not exceed 50 characters."); 
    }

    [Fact(DisplayName = "Password Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the Password must not be empty.")]
    public void Password_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<UserLoginInbound>()
            .With(x => x.Password, string.Empty)
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == nameof(inbound.Password));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Password is required.");
    }

    [Fact(DisplayName = "Password Must Be At Least 8 Characters Long")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the Password must be at least 8 characters long.")]
    public void Password_MustBeAtLeast8CharactersLong()
    {
        // Arrange
        var inbound = _fixture.Build<UserLoginInbound>()
            .With(x => x.Password, "1234567")
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.Password));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Password must be at least 8 characters long.");
    }

    [Fact(DisplayName = "Password Must Not Exceed 100 Characters")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the Password must not exceed 100 characters.")]
    public void Password_MustNotExceed100Characters()
    {
        // Arrange
        var inbound = _fixture.Build<UserLoginInbound>()
            .With(x => x.Password, new string('a', 101))
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.Password));
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "Password must not exceed 100 characters.");
    }

    [Fact(DisplayName = "Valid Inbound")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UserLogin")]
    [Trait("Description", "Ensure that the inbound is valid.")]
    public void ValidInbound()
    {
        // Arrange
        var inbound = new Faker<UserLoginInbound>()
            .CustomInstantiator(faker => new UserLoginInbound
            (
                faker.Internet.UserName(),
                faker.Internet.Password()
            ));

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}
