using AspNetCoreAddin.Data.Enums;
using AspNetCoreAddin.Data.Interfaces;
using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreAddin.Data.Entities
{
    [Table("Products")]
    public class Product : DomainEntity<int>, IHasSeoMetaData, ISwitchable, IDateTracking
    {
        public Product()
        {
        }

        public Product(ProductViewModel productVm)
        {
            Name = productVm.Name;
            CategoryId = productVm.CategoryId;
            Price = productVm.Price;
            Description = productVm.Description;
            Content = productVm.Content;
            HomeFlag = productVm.HomeFlag;
            HotFlag = productVm.HotFlag;
            Tag = productVm.Tag;
            Status = productVm.Status;
            SeoPageTitle = productVm.SeoPageTitle;
            SeoAlias = productVm.SeoAlias;
            SeoKeywords = productVm.SeoKeywords;
            SeoDescription = productVm.SeoDescription;
            ThumbnailImage = productVm.ThumbnailImage;
            VideoTube = productVm.VideoTube;
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string Content { get; set; }

        [MaxLength(500)]
        public string ThumbnailImage { get; set; }

        public bool? HomeFlag { get; set; }

        public bool? HotFlag { get; set; }

        [MaxLength(255)]
        public string Tag { set; get; }

        [MaxLength(255)]
        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public Status Status { set; get; }

        [MaxLength(500)]
        public string SeoPageTitle { set; get; }

        [Column(TypeName = "varchar(255)")]
        public string SeoAlias { set; get; }

        [MaxLength(500)]
        public string SeoKeywords { set; get; }

        [MaxLength(500)]
        public string SeoDescription { set; get; }

        [MaxLength(500)]
        public string VideoTube { set; get; }

        public virtual ICollection<Comment> Comments { set; get; }
    }
}