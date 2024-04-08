using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TaskFlowHub.Infrastructure.Security;

/// <summary>
/// Generates JWT tokens for user login.
/// </summary>
/// <param name="jwtTokenSettings">The JWT token settings.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="IUserLoginTokenGenerator" />
/// <seealso cref="JwtTokenSettings" />
/// <seealso cref="User" />
/// <remarks>
/// This class generates JWT tokens for user login.
/// </remarks>
public class JwtTokenGenerator(
    IOptions<JwtTokenSettings> jwtTokenSettings,
    ILogger<JwtTokenGenerator> logger) : IUserLoginTokenGenerator
{
    private readonly JwtTokenSettings _jwtTokenSettings = jwtTokenSettings.Value;
    private readonly ILogger<JwtTokenGenerator> _logger = logger;

    public string GenerateToken(User user)
    {
        _logger.LogDebug("Generating token for user {UserId}.", user.Id);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var handler = new JwtSecurityTokenHandler();

        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Audience = _jwtTokenSettings.Audience,
            Expires = DateTime.UtcNow.AddMinutes(_jwtTokenSettings.ExpiryInMinutes),
            Issuer = _jwtTokenSettings.Issuer,
            NotBefore = DateTime.UtcNow,
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity(claims),
            TokenType = "at+jwt"
        });

        var token = handler.WriteToken(securityToken);

        _logger.LogInformation("Token generated for user {UserId}.", user.Id);

        return token;
    }
}
