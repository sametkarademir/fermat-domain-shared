using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Fermat.Domain.Shared.Filters;

namespace Fermat.Domain.Shared.Conventions;

/// <summary>
/// A convention that disables all actions in a controller.
/// </summary>
/// <param name="controllerType">The type of the controller to disable.</param>
/// <remarks>
/// This convention is used to disable all actions in a controller.
/// It is used to disable controllers that are not intended to be used.
/// </remarks>
public class ControllerDisablingConvention(Type controllerType) : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (controller.ControllerType != controllerType)
        {
            return;
        }

        foreach (var action in controller.Actions)
        {
            action.Filters.Add(new DisableApiFilter());
        }
    }
}