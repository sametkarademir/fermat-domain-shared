using Fermat.Domain.Shared.Interfaces;

namespace Fermat.Domain.Shared.DTOs;

/// <summary>
/// Base class for fully audited entity data transfer objects.
/// Contains creation, modification, and deletion audit information.
/// </summary>
[Serializable]
public abstract class FullAuditedEntityDto : AuditedEntityDto, IFullAuditedObject
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who deleted the entity.
    /// </summary>
    public Guid? DeleterId { get; set; }

    /// <summary>
    /// Gets or sets the deletion time of the entity.
    /// </summary>
    public DateTime? DeletionTime { get; set; }
}

/// <summary>
/// Base class for fully audited entity data transfer objects with a specific key type.
/// Contains creation, modification, and deletion audit information.
/// </summary>
/// <typeparam name="TKey">The type of the entity's key.</typeparam>
[Serializable]
public abstract class FullAuditedEntityDto<TKey> : AuditedEntityDto<TKey>, IFullAuditedObject
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who deleted the entity.
    /// </summary>
    public Guid? DeleterId { get; set; }

    /// <summary>
    /// Gets or sets the deletion time of the entity.
    /// </summary>
    public DateTime? DeletionTime { get; set; }
}