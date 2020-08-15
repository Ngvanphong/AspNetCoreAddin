using AspNetCoreAddin.Data.Enums;
using AspNetCoreAddin.Data.Interfaces;
using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCoreAddin.Data.Entities
{
    [Table("Functions")]
    public class Function : DomainEntity<string>, ISwitchable, ISortTable
    {
        public Function()
        {

        }
        public Function(FunctionViewModel functionVm)
        {
            Name = functionVm.Name;
            URL = functionVm.URL;
            ParentId = functionVm.ParentId;
            IconCss = functionVm.IconCss;
            SortOrder = functionVm.SortOrder;
            Status = Status.Active;
        }
        [Required]
        [StringLength(128)]
        public string Name { set; get; }

        [Required]
        [StringLength(250)]
        public string URL { set; get; }


        [StringLength(128)]
        public string ParentId { set; get; }

        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
    }
}
