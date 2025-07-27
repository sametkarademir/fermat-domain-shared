namespace Fermat.Domain.Shared.Filters;

/// <summary>
/// Indicates that a class or property should be excluded from processing.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true)]
public class ExcludeFromProcessingAttribute : Attribute
{
}