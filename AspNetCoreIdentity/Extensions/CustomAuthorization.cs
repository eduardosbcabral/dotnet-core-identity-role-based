using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AspNetCoreIdentity.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidarRolesUsuario(HttpContext context, RoleManager<PerfilAcesso> roleManager, string roleName, string roleValue)
        {
            var role = roleManager
                .Roles
                .FirstOrDefault(x => x.Name == roleName);

            if (role is null) return false;

            var claims = roleManager.GetClaimsAsync(role).Result;

            if (claims is null) return false;

            return context.User.Identity.IsAuthenticated &&
                   claims.Any(x => x.Value.Contains(roleValue));
        }
    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;
        private readonly RoleManager<PerfilAcesso> _roleManager;

        public RequisitoClaimFilter(Claim claim, RoleManager<PerfilAcesso> roleManager)
        {
            _claim = claim;
            _roleManager = roleManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "", page = "/Identity/Signin", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
                return;
            }

            if (!CustomAuthorization.ValidarRolesUsuario(context.HttpContext, _roleManager, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}