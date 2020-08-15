using AspNetCoreAddin.Data.Enums;
using System;
using System.Collections.Generic;

namespace AspNetCoreAddin.Data.ViewModels
{
    public class BlogViewModel
    {
        public int Id { get; set; }

        public string Name { set; get; }

        public string Image { set; get; }

        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public int? ViewCount { set; get; }

        public string Tags { get; set; }

        public virtual ICollection<BlogTagViewModel> BlogTags { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; }

        public string SeoPageTitle { set; get; }

        public string SeoAlias { set; get; }

        public string SeoKeywords { set; get; }

        public string SeoDescription { set; get; }
    }
}