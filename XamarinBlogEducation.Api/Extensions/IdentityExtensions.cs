
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace XamarinBlogEducation.Api.Extensions
{
    public static class IdentityExtensions
    {

        public static string GetUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.NameIdentifier);
            return (claim != null) ? claim.Value : string.Empty;
        }

        //public static string GetUserId(this IIdentity identity)
        //{
        //    ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
        //    Claim claim = claimsIdentity?.FindFirst();
        //    if (claim == null)
        //    {
        //        return string.Empty;
        //    }
        //    return claim.Value;
        //}
        
    }
}
