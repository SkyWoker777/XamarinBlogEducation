using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
namespace XamarinBlogEducation.Api.Extensions
{
    public static class IdentityExtensions
    {

        public static string GetUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Id");
            return (claim != null) ? claim.Value : string.Empty;
        }

        //public static string GetUserId(this IIdentity identity)
        //{
        //    ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
        //    Claim claim = claimsIdentity?.FindFirst(JwtClaimIdentifiers.Id);
        //    if (claim == null)
        //    {
        //        return string.Empty;
        //    }
        //    return claim.Value;
        //}

        //public static string GetUserName(this IIdentity identity);
        // public static string FindFirstValue(this ClaimsIdentity identity, string claimType);
    }
}
