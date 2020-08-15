using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.Enums;
using System;
using System.Collections.Generic;

namespace AspNetCoreAddin.Data.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ThumbnailImage { get; set; }

        public bool? HomeFlag { get; set; }

        public bool? HotFlag { get; set; }

        public string Tag { get; set; }

        public ProductCategoryViewModel ProductCategory { set; get; }

        public string SeoPageTitle { set; get; }

        public string SeoAlias { set; get; }

        public string SeoKeywords { set; get; }

        public string SeoDescription { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }

        public Status Status { set; get; }

        public string VideoTube { set; get; }


    }
}