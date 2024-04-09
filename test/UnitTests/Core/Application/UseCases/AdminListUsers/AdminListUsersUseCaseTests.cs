namespace TaskFlowHub.UnitTests.Core.Application.UseCases.AdminListUsers;

public class AdminListUsersUseCaseTests
{
    private readonly IFixture _fixture;

    private readonly Mock<IAdminListUsersRepository> _repository;

    private readonly Mock<IAdminListUsersOutcomeHandler> _outcomeHandler;

    private readonly AdminListUsersUseCase _sut;

    public AdminListUsersUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _repository = _fixture.Freeze<Mock<IAdminListUsersRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IAdminListUsersOutcomeHandler>>();

        _sut = new AdminListUsersUseCase(_repository.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler 'UserIsNotAnAdministrator' Must Be Called When Requester Is Not An Administrator")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "AdminListUsers")]
    [Trait("Description", "Ensure that the 'UserIsNotAnAdministrator' outcome handler is called when the requester is not an administrator.")]
    public async Task UserIsNotAnAdministrator_OutcomeHandlerMustBeCalledWhenRequesterIsNotAnAdministrator()
    {
        // Arrange
        var requesterUserId = _fixture.Create<Guid>();

        _repository
            .Setup(x => x.IsUserAnAdministratorAsync(requesterUserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _outcomeHandler
            .Setup(x => x.UserIsNotAnAdministrator()).Verifiable();

        // Act
        await _sut.ExecuteAsync(requesterUserId, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.UserIsNotAnAdministrator(), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler 'UsersListed' Must Be Called When Users Are Listed")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "AdminListUsers")]
    [Trait("Description", "Ensure that the 'UsersListed' outcome handler is called when the users are listed.")]
    public async Task UsersListed_OutcomeHandlerMustBeCalledWhenUsersAreListed()
    {
        // Arrange
        var requesterUserId = _fixture.Create<Guid>();
        var users = _fixture.CreateMany<User>().ToList();

        _repository
            .Setup(x => x.IsUserAnAdministratorAsync(requesterUserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _repository
            .Setup(x => x.ListUsersAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        _outcomeHandler
            .Setup(x => x.UsersListed(users)).Verifiable();

        // Act
        await _sut.ExecuteAsync(requesterUserId, CancellationToken.None);

        // Assert
        _outcomeHandler.Verify(x => x.UsersListed(users), Times.Once);
    }
}
