using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AspNetCoreIdentity.Extensions
{
    public static class ClaimsIdentityExtension
    {
        public static List<Claim> Roles(this ClaimsIdentity identity)
        {
            return identity.Claims
                           .Where(c => c.Type == ClaimTypes.Role)
                           .ToList();
        }
    }
}
