using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.ViewModels
{
   public class BlogTagViewModel
    {
        public int Id { get; set; }
        public int BlogId { set; get; }

        public string TagId { set; get; }
    }
}
