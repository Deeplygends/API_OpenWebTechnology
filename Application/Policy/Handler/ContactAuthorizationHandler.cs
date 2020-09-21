using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Policy.Requirement;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Application.Policy.Handler
{
   public class ContactAuthorizationHandler : AuthorizationHandler<SameUserRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SameUserRequirement requirement,
            string resource)
        {
            if (context.User.Identity.Name == resource)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
