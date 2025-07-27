using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fermat.Domain.Shared.Conventions;

/// <summary>
/// Configuration class for endpoint-specific authorization settings
/// </summary>
public class EndpointOptions
{
    /// <summary>
    /// Indicates whether the endpoint is enabled
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Name of the action to which this configuration applies
    /// </summary>
    public string ActionName { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the endpoint requires authentication
    /// </summary>
    public bool RequireAuthentication { get; set; } = true;

    /// <summary>
    /// Authorization policy to apply to the endpoint
    /// </summary>
    public string? Policy { get; set; }

    /// <summary>
    /// List of roles that are allowed to access the endpoint
    /// </summary>
    public List<string>? Roles { get; set; }
}

public class AuthorizationOptions
{
    /// <summary>
    /// If true, all endpoints require authentication even if no policy is specified
    /// </summary>
    public bool RequireAuthentication { get; set; } = true;

    /// <summary>
    /// Global authorization policy to apply to all endpoints
    /// </summary>
    public string? Policy { get; set; }

    /// <summary>
    /// List of endpoint roles that are allowed to access the endpoints
    /// </summary>
    public List<string>? Roles { get; set; }
}

/// <summary>
/// This class is used to apply authorization policies to controllers.
/// It implements the IControllerModelConvention interface.
/// </summary>
/// <param name="controllerType">The type of the controller to apply the convention to.</param>
/// <param name="route">The route template to apply to the controller.</param>
/// <param name="authorizationOptions">Global authorization options to apply to the controller.</param>
/// <param name="endpointOptions">Endpoint-specific authorization configurations.</param>
public class ControllerAuthorizationConvention(
    Type controllerType,
    string route,
    AuthorizationOptions authorizationOptions,
    List<EndpointOptions>? endpointOptions = null)
    : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (controller.ControllerType != controllerType)
        {
            return;
        }

        foreach (var selector in controller.Selectors)
        {
            if (selector.AttributeRouteModel != null)
            {
                selector.AttributeRouteModel.Template = route;
            }
        }

        var existingFilters = controller.Filters
            .OfType<IAuthorizationFilter>()
            .Where(f => f is AuthorizeFilter)
            .ToList();

        foreach (var filter in existingFilters)
        {
            controller.Filters.Remove(filter);
        }

        // Apply endpoint-specific configurations
        if (endpointOptions != null)
        {
            ApplyEndpointConfigurations(controller);
        }

        // Apply global controller-level authorization
        ApplyGlobalAuthorization(controller);
    }

    private void ApplyGlobalAuthorization(ControllerModel controller)
    {
        if (authorizationOptions.RequireAuthentication && authorizationOptions.Roles is { Count: > 0 })
        {
            controller.Filters.Add(new AuthorizeFilter(
                [
                    new AuthorizeAttribute
                    {
                        Roles = string.Join(",", authorizationOptions.Roles)
                    }
                ]
            ));
        }
        else if (authorizationOptions.RequireAuthentication && !string.IsNullOrEmpty(authorizationOptions.Policy))
        {
            controller.Filters.Add(new AuthorizeFilter(authorizationOptions.Policy));
        }
        else if (authorizationOptions.RequireAuthentication)
        {
            controller.Filters.Add(new AuthorizeFilter());
        }
        else
        {
            controller.Filters.Add(new AllowAnonymousFilter());
        }
    }

    private void ApplyEndpointConfigurations(ControllerModel controller)
    {
        var actionsToProcess = controller.Actions.ToList();
        foreach (var action in actionsToProcess)
        {
            var endpointOption = endpointOptions?.FirstOrDefault(ec =>
                ec.ActionName.Equals(action.ActionName, StringComparison.OrdinalIgnoreCase));

            if (endpointOption == null)
            {
                continue;
            }

            if (!endpointOption.IsEnabled)
            {
                controller.Actions.Remove(action);
                continue;
            }

            var existingActionFilters = action.Filters
                .OfType<IAuthorizationFilter>()
                .ToList();

            foreach (var filter in existingActionFilters)
            {
                action.Filters.Remove(filter);
            }

            if (endpointOption.RequireAuthentication && endpointOption.Roles is { Count: > 0 })
            {
                action.Filters.Add(new AuthorizeFilter(
                    [
                        new AuthorizeAttribute
                        {
                            Roles = string.Join(",", endpointOption.Roles)
                        }
                    ]
                ));
            }
            else if (endpointOption.RequireAuthentication && !string.IsNullOrEmpty(endpointOption.Policy))
            {
                action.Filters.Add(new AuthorizeFilter(endpointOption.Policy));
            }
            else if (endpointOption.RequireAuthentication)
            {
                action.Filters.Add(new AuthorizeFilter());
            }
            else
            {
                action.Filters.Add(new AllowAnonymousFilter());
            }
        }
    }
}