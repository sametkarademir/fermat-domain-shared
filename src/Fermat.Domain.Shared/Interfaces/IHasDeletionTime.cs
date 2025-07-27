namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Interfaces for objects that track their deletion time.
/// </summary>
public interface IHasDeletionTime : ISoftDelete
{
    /// <summary>
    /// Gets or sets the deletion time of the entity.
    /// </summary>
    DateTime? DeletionTime { get; set; }
}