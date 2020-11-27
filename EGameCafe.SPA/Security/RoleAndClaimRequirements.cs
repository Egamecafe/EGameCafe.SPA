using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Security
{
    public class RoleAndClaimRequirements : IAuthorizationRequirement
    {
        public RoleAndClaimRequirements(string claimType, string role)
        {
            ClaimType = claimType;
            Role = role;
        }

        public string ClaimType { get;  }
        public string Role { get; }
    }

    public class CustomrequireClaimAndRoleHandler : AuthorizationHandler<RoleAndClaimRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAndClaimRequirements requirement)
        {
            var hasClaim = context.User.Claims.FirstOrDefault(e => e.Type == requirement.ClaimType);
            var hasRole = context.User.IsInRole(requirement.Role);

            if(hasClaim != null || hasRole)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public static class AuthorizationPolicyBuilderExtentions
    {
        public static AuthorizationPolicyBuilder RequireCustomRoleAndClaim(this AuthorizationPolicyBuilder builder
            , string claimType, string role)
        {
            builder.AddRequirements(new RoleAndClaimRequirements(claimType, role));
            return builder;
        }
    }
}
