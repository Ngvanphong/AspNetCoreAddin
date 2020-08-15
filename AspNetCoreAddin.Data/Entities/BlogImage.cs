using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCoreAddin.Data.Entities
{
    [Table("BlogImages")]
    public class BlogImage : DomainEntity<int>
    {
        public BlogImage()
        {

        }
        public BlogImage(BlogImageViewModel blogImageVm)
        {
            BlogId = blogImageVm.BlogId;
            Path = blogImageVm.Path;
            Caption = blogImageVm.Caption;
        }
        public int BlogId { get; set; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }

        [StringLength(250)]
        public string Path { get; set; }

        [StringLength(250)]
        public string Caption { get; set; }
    }
}
