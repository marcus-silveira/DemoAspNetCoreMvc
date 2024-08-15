using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.Extensions;

public static class CustomAuthorization
{
    public static bool ValidateUserClaims(HttpContext context, string claimName, string claimValue)
    {
        if (context.User.Identity == null) throw new InvalidOperationException();

        return context.User.Identity.IsAuthenticated &&
               context.User.Claims.Any(c => c.Type == claimName && c.Value.Split(',').Contains(claimValue));
    }
}

public class RequiredClaimFilter(Claim claim) : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity == null) throw new InvalidOperationException();

        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString()
            }));
            return;
        }

        if (!CustomAuthorization.ValidateUserClaims(context.HttpContext, claim.Type, claim.Value))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequiredClaimFilter))
    {
        Arguments = new object[] { new Claim(claimName, claimValue) };
    }
}