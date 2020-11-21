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

        public static void UpdateUser(this AppUser appUser, AppUserViewModel appUserVm)
        {
            appUser.Address = appUserVm.Address;
            appUser.Avatar = appUserVm.Avatar;
            appUser.BirthDay = appUserVm.BirthDay;
            appUser.Email = appUserVm.Email;
            appUser.Gender = appUserVm.Gender;
            appUser.FullName = appUserVm.FullName;
            appUser.Status = appUserVm.Status;
            appUser.PhoneNumber = appUserVm.PhoneNumber;
            appUser.UserName = appUserVm.UserName;
           
        }

    }
}
