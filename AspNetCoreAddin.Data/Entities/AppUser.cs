using AspNetCoreAddin.Data.Enums;
using AspNetCoreAddin.Data.ViewModels.Indentity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCoreAddin.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser() : base()
        {

        }
        public AppUser(AppUserViewModel appUserVm):base( appUserVm.UserName)
        {
            FullName = appUserVm.FullName;
            BirthDay = appUserVm.BirthDay;
            Avatar = appUserVm.Avatar;
            Status = appUserVm.Status;
            PhoneNumber = appUserVm.PhoneNumber;
            Email = appUserVm.Email;
            DateCreated = appUserVm.DateCreated;
            DateModified = appUserVm.DateModified;
        }

        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }

        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Status Status { get; set; }

        public string Address { get; set; }

        public bool? Gender { get; set; }
    }
}
