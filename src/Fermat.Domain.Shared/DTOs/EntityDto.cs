using Fermat.Domain.Shared.Interfaces;

namespace Fermat.Domain.Shared.DTOs;

/// <summary>
/// Base class for all entity data transfer objects.
/// Provides a foundation for DTOs that represent domain entities.
/// </summary>
[Serializable]
public abstract class EntityDto : IEntityDto
{
}

/// <summary>
/// Base class for entity data transfer objects with a specific key type.
/// Provides a foundation for DTOs that represent domain entities with typed identifiers.
/// </summary>
/// <typeparam name="TKey">The type of the entity's key.</typeparam>
[Serializable]
public abstract class EntityDto<TKey> : EntityDto, IEntityDto<TKey>
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    public TKey Id { get; set; } = default!;
}