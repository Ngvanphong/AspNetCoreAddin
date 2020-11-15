using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels.Indentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAddin.WebApi.Extensions
{
    public static class UpdateEntity
    {
        public static void UpdateAppRole(this AppRole appRole,AppRoleViewModel appRoleVm)
        {
            appRole.Name = appRoleVm.Name;
            appRole.Description = appRoleVm.Description;
        }
    }
}
