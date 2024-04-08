namespace TaskFlowHub.Infrastructure.Security;

/// <summary>
/// Represents the JWT token settings.
/// </summary>
/// <param name="Audience">The audience.</param>
/// <param name="ExpiryInMinutes">The expiry in minutes.</param>
/// <param name="Issuer">The issuer.</param>
/// <param name="Secret">The secret.</param>
/// <remarks>
/// This record represents the JWT token settings.
/// </remarks>
public class JwtTokenSettings
{
    public string Audience { get; init; } = default!;
    public int ExpiryInMinutes { get; init; }
    public string Issuer { get; init; } = default!;
    public string Secret { get; init; } = default!;
}
