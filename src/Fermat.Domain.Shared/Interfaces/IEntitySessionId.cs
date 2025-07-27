namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Interfaces for objects that have a session identifier.
/// Used for tracking operations within a specific session.
/// </summary>
public interface IEntitySessionId
{
    /// <summary>
    /// Gets or sets the session identifier for tracking operations within a session.
    /// </summary>
    Guid? SessionId { get; set; }
}