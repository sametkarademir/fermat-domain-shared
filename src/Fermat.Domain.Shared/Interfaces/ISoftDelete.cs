namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Interfaces for objects that support soft deletion.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}