namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Interfaces for objects that track creation information.
/// Combines creation time and creator interfaces.
/// </summary>
public interface ICreationAuditedObject : IHasCreationTime, IMayHaveCreator
{
}