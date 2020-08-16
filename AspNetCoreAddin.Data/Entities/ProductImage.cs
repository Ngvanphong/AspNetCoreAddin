using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCoreAddin.Data.Entities
{

    [Table("ProductImages")]
    public class ProductImage : DomainEntity<int>
    {
        public ProductImage()
        {

        }
        public ProductImage(ProductImageViewModel productImageVm)
        {
            ProductId = productImageVm.Id;
            Path = productImageVm.Path;
            Caption = productImageVm.Caption;
            SwitchImage = productImageVm.SwitchImage;
        }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [StringLength(250)]
        public string Path { get; set; }

        [StringLength(250)]
        public string Caption { get; set; }

        public bool SwitchImage { get; set; }
    }
}
