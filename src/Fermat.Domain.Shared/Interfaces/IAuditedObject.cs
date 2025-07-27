namespace Fermat.Domain.Shared.Interfaces;

/// <summary>
/// Interfaces for audited objects that track creation and modification information.
/// Combines creation and modification audit interfaces.
/// </summary>
public interface IAuditedObject :
    ICreationAuditedObject,
    IHasCreationTime,
    IMayHaveCreator,
    IModificationAuditedObject,
    IHasModificationTime
{
}