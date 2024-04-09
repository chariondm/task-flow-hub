namespace TaskFlowHub.Adapters.Inbounds.TaskFlowHubHttpApi.Modules.Common.Infrastructure.Authentication;

/// <summary>
/// Interface for accessing the user context.
/// </summary>
public interface IUserContextAccessor
{
    /// <summary>
    /// Gets the user ID from the current context.
    /// </summary>
    /// <returns>The user ID from the current context.</returns>
    Guid GetUserId();

    /// <summary>
    /// Checks if the user is an admin.
    /// </summary>
    /// <returns>True if the user is an admin, false otherwise.</returns>
    /// <remarks>
    /// This method checks if the user is an admin.
    /// </remarks>
    public bool IsAdmin();
}
