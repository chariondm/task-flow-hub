namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Modules.Common.Infrastructure.Authentication;

/// <summary>
/// Accessor for the user context.
/// </summary>
public class UserContextAccessor(IHttpContextAccessor httpContextAccessor) : IUserContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// Gets the user ID from the current context.
    /// </summary>
    public Guid GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return userId is null ? Guid.Empty : Guid.Parse(userId);
    }

    /// <summary>
    /// Checks if the user is an admin.
    /// </summary>
    /// <returns>True if the user is an admin, false otherwise.</returns>
    /// <remarks>
    /// This method checks if the user is an admin.
    /// </remarks>
    public bool IsAdmin()
    {
        var isAdmin = _httpContextAccessor.HttpContext?.User.IsInRole("admin");

        return isAdmin ?? false;
    }
}
