﻿using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCoreAddin.Data.Entities
{
    [Table("Permissions")]
    public class Permission : DomainEntity<int>
    {
        public Permission()
        {
        }
        public Permission(PermissionViewModel permissionVm)
        {
            RoleId = permissionVm.RoleId;
            FunctionId = permissionVm.FunctionId;
            CanCreate = permissionVm.CanCreate;
            CanRead = permissionVm.CanRead;
            CanUpdate = permissionVm.CanUpdate;
            CanDelete = permissionVm.CanDelete;
        }

        public Guid RoleId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FunctionId { get; set; }

        public bool CanCreate { set; get; }
        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }
        public bool CanDelete { set; get; }

        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { get; set; }

        [ForeignKey("FunctionId")]
        public virtual Function Function { get; set; }
    }
}
