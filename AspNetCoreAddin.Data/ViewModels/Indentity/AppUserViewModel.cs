using AspNetCoreAddin.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.ViewModels.Indentity
{
   public class AppUserViewModel
    {

        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { set; get; }

        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }


        public string Avatar { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Status Status { get; set; }

        public bool? Gender { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        public virtual ICollection<string> Roles { get; set; }
    }
}
