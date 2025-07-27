using Fermat.Domain.Shared.Interfaces;

namespace Fermat.Domain.Shared.DTOs;

/// <summary>
/// Base class for creation audited entity data transfer objects.
/// Contains creation time and creator user information.
/// </summary>
[Serializable]
public abstract class CreationAuditedEntityDto : EntityDto, ICreationAuditedObject
{
    /// <summary>
    /// Gets or sets the creation time of the entity.
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who created the entity.
    /// </summary>
    public Guid? CreatorId { get; set; }
}

/// <summary>
/// Base class for creation audited entity data transfer objects with a specific key type.
/// Contains creation time and creator user information.
/// </summary>
/// <typeparam name="TKey">The type of the entity's key.</typeparam>
[Serializable]
public abstract class CreationAuditedEntityDto<TKey> : EntityDto<TKey>, ICreationAuditedObject
{
    /// <summary>
    /// Gets or sets the creation time of the entity.
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who created the entity.
    /// </summary>
    public Guid? CreatorId { get; set; }
}