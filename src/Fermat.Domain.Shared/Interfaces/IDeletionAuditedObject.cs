namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Interface for objects that track deletion information.
/// Combines deletion time and soft delete interfaces.
/// </summary>
public interface IDeletionAuditedObject : IHasDeletionTime, ISoftDelete
{
    /// <summary>
    /// Gets or sets the unique identifier of the user who deleted the entity.
    /// </summary>
    Guid? DeleterId { get; set; }
}