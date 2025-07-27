using Fermat.Domain.Shared.Interfaces;
using Fermat.Domain.Shared.Filters;

namespace Fermat.Domain.Shared.Auditing;

/// <summary>
/// Base class for audited entities.
/// Contains creation, modification time and user information.
/// </summary>
[Serializable]
public abstract class AuditedEntity : CreationAuditedEntity, IAuditedObject, IHasConcurrencyStamp
{
    /// <summary>
    /// Gets or sets the last modification time of the entity.
    /// </summary>
    [ExcludeFromProcessing]
    public virtual DateTime? LastModificationTime { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who last modified the entity.
    /// </summary>
    [ExcludeFromProcessing]
    public virtual Guid? LastModifierId { get; set; }

    /// <summary>
    /// Gets or sets the concurrency stamp for optimistic concurrency control.
    /// </summary>
    [ExcludeFromProcessing]
    public virtual string ConcurrencyStamp { get; set; } = string.Empty;
}

/// <summary>
/// Base class for audited entities with a specific key type.
/// Contains creation, modification time and user information.
/// </summary>
/// <typeparam name="TKey">The type of the entity's key.</typeparam>
[Serializable]
public abstract class AuditedEntity<TKey> : CreationAuditedEntity<TKey>, IAuditedObject, IHasConcurrencyStamp
{
    /// <summary>
    /// Gets or sets the last modification time of the entity.
    /// </summary>
    [ExcludeFromProcessing]
    public virtual DateTime? LastModificationTime { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who last modified the entity.
    /// </summary>
    [ExcludeFromProcessing]
    public virtual Guid? LastModifierId { get; set; }

    /// <summary>
    /// Gets or sets the concurrency stamp for optimistic concurrency control.
    /// </summary>
    [ExcludeFromProcessing]
    public virtual string ConcurrencyStamp { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditedEntity{TKey}"/> class.
    /// </summary>
    protected AuditedEntity()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditedEntity{TKey}"/> class with a specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    protected AuditedEntity(TKey id) : base(id)
    {
    }
}