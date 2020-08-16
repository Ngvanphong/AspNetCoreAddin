using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.ViewModels
{
   public class ProductImageViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual ProductViewModel Product { get; set; }


        public string Path { get; set; }

        public string Caption { get; set; }

        public bool SwitchImage { get; set; }
    }
}
