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
    [Table("ProductCategories")]
    public class ProductCategory : DomainEntity<int>,IHasSeoMetaData, ISwitchable, ISortTable, IDateTracking
    {
        public ProductCategory()
        {
            Products = new List<Product>();
        }

        public ProductCategory(ProductCategoryViewModel productCategoryVm)
        {
            Name = productCategoryVm.Name;
            Description = productCategoryVm.Description;
            HomeOrder = productCategoryVm.HomeOrder;
            Image = productCategoryVm.Image;
            SortOrder = productCategoryVm.SortOrder;
            Status = productCategoryVm.Status;
            SeoPageTitle = productCategoryVm.SeoPageTitle;
            SeoAlias = productCategoryVm.SeoAlias;
            SeoKeywords = productCategoryVm.SeoKeywords;
            SeoDescription = productCategoryVm.SeoDescription;
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public int? HomeOrder { get; set; }

        [MaxLength(255)]
        public string Image { get; set; }


        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }

        [MaxLength(500)]
        public string SeoPageTitle { set; get; }

        [Column(TypeName = "varchar(255)")]
        public string SeoAlias { set; get; }

        [MaxLength(500)]
        public string SeoKeywords { set; get; }

        [MaxLength(500)]
        public string SeoDescription { set; get; }

        public virtual ICollection<Product> Products { set; get; }
        
    }
}
