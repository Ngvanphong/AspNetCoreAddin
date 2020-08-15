using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCoreAddin.Data.Entities
{
    [Table("Subcribles")]
    public class Subcrible : DomainEntity<int>
    {
        public Subcrible()
        {

        }
        public Subcrible(SubcribleViewModel subcribleVm)
        {
            Email = subcribleVm.Email;
        }
        [MaxLength(255)]
        public string Email { get; set; }
    }
}
