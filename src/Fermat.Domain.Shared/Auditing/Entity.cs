using Fermat.Domain.Shared.Interfaces;
using Fermat.Domain.Shared.Filters;

namespace Fermat.Domain.Shared.Auditing;

/// <summary>
/// Base class for all domain entities.
/// Provides a foundation for domain entities with basic entity functionality.
/// </summary>
[Serializable]
public abstract class Entity : IEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    protected Entity()
    {
    }
}

/// <summary>
/// Base class for domain entities with a specific key type.
/// Provides a foundation for domain entities with typed identifiers and basic entity functionality.
/// </summary>
/// <typeparam name="TKey">The type of the entity's key.</typeparam>
[Serializable]
public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    [ExcludeFromProcessing]
    public virtual TKey Id { get; set; } = default!;

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TKey}"/> class.
    /// </summary>
    protected Entity()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TKey}"/> class with a specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    protected Entity(TKey id)
    {
        Id = id;
    }
}