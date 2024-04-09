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
}
