using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.Filters;

public class AuditFilter(ILogger<AuditFilter> logger) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.User.Identity!.IsAuthenticated) return;
        var message = context.HttpContext.User.Identity.Name + " Acessou: " +
                      context.HttpContext.Request.GetDisplayUrl();

        logger.LogWarning(message);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}