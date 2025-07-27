namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Interfaces for objects that have a snapshot identifier.
/// Used for tracking entity state snapshots.
/// </summary>
public interface IEntitySnapshotId
{
    /// <summary>
    /// Gets or sets the snapshot identifier for tracking entity state snapshots.
    /// </summary>
    Guid? SnapshotId { get; set; }
}