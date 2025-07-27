namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Interfaces for objects that have a correlation identifier.
/// Used for tracking related operations across the system.
/// </summary>
public interface IEntityCorrelationId
{
    /// <summary>
    /// Gets or sets the correlation identifier for tracking related operations.
    /// </summary>
    Guid? CorrelationId { get; set; }
}