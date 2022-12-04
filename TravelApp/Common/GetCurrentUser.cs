using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Common
{
    public static class GetCurrentUser
    {
        public static string GetCurrentUserId(this ClaimsPrincipal claimsPrincipalUser)
        {
            return claimsPrincipalUser.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string GetCurrentUserName(this ClaimsPrincipal claimsPrincipalUser)
        {
            return claimsPrincipalUser.FindFirstValue(ClaimTypes.Name);
        }
    }
}
