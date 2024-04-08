namespace TaskFlowHub.Core.Application.UseCases.Users.UserLogin.Outbounds;

public interface IUserLoginTokenGenerator
{
    /// <summary>
    /// Generates a token for the specified user.
    /// </summary>
    /// <param name="user">The user to generate the token for.</param>
    /// <returns>The generated token.</returns>
    /// <remarks>
    /// This method generates a token for the specified user.
    /// </remarks>
    string GenerateToken(User user);
}
