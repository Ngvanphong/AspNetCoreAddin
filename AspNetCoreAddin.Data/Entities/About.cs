using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCoreAddin.Data.Entities
{
    [Table("Abouts")]
    public class About : DomainEntity<int>
    {
        public About()
        {

        }
        public About(AboutViewModel aboutVm)
        {
            Content = aboutVm.Content;
        }
        public string Content { set; get; }
    }
}
