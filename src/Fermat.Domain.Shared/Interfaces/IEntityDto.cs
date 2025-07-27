namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Base interface for all entity data transfer objects.
/// </summary>
public interface IEntityDto
{
}

/// <summary>
/// Base interface for entity data transfer objects with a specific key type.
/// </summary>
/// <typeparam name="TKey">The type of the entity's key.</typeparam>
public interface IEntityDto<TKey> : IEntityDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    TKey Id { get; set; }
}