namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Interfaces for objects that support optimistic concurrency control.
/// </summary>
public interface IHasConcurrencyStamp
{
    /// <summary>
    /// Gets or sets the concurrency stamp for optimistic concurrency control.
    /// </summary>
    string ConcurrencyStamp { get; set; }
}