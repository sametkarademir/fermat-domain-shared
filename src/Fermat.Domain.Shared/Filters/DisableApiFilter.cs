using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fermat.Domain.Shared.Filters;

/// <summary>
/// Disables the API by returning a 404 Not Found status code.
/// </summary>
/// <remarks>
/// This filter can be applied to a controller or action method to disable it.
/// It will return a 404 Not Found status code for any request to the controller or action method.
/// </remarks>
/// <example>
/// <code>
/// [DisableApiFilter]
/// public class MyController : ControllerBase
/// {
///     [HttpGet]
///     public IActionResult Get()
///     {
///         return Ok("This will not be executed.");
///     }
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DisableApiFilter : Attribute, IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Result = new NotFoundResult();
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }
}