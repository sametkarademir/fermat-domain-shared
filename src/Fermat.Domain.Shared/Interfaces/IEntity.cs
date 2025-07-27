namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Base interface for all domain entities.
/// </summary>
public interface IEntity
{
}

/// <summary>
/// Base interface for domain entities with a specific key type.
/// </summary>
/// <typeparam name="TKey">The type of the entity's key.</typeparam>
public interface IEntity<TKey> : IEntity
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    TKey Id { get; }
}