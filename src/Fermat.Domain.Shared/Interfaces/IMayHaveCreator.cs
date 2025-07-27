namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Interfaces for objects that may have a creator user.
/// </summary>
public interface IMayHaveCreator
{
    /// <summary>
    /// Gets or sets the unique identifier of the user who created the entity.
    /// </summary>
    Guid? CreatorId { get; set; }
}