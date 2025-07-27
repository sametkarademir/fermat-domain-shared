using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Fermat.Domain.Shared.Conventions;

public class ControllerRemovalConvention(Type controllerType) : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (controller.ControllerType != controllerType)
        {
            return;
        }

        controller.Actions.Clear();
        controller.Selectors.Clear();
        controller.Filters.Clear();
    }
}