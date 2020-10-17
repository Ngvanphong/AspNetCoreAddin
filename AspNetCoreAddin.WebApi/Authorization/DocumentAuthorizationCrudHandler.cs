using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Utilities.Constants;
using AspNetCoreAddin.WebApi.ServiceLocators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreAddin.WebApi.Authorization
{
    public class DocumentAuthorizationCrudHandler : AuthorizationHandler<OperationAuthorizationRequirement, string>
    {
        private readonly IFunctionService _functionService = ServiceLocator.Current.GetInstance<IFunctionService>();

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, string resource)
        {
            var roles = ((ClaimsIdentity)context.User.Identity).Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            if (roles != null)
            {
                var listRole = roles.Value.Split(";");
                var hasPermission = await _functionService.CheckPermission(resource, requirement.Name, listRole);
                if (hasPermission || listRole.Contains(CommonConstants.Admin))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }

        }
    }
}
