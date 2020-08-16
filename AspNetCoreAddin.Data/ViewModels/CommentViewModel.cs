using AspNetCoreAddin.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.ViewModels
{
    public class CommentViewModel
    {
        public int Id { set; get; }
        public int ProductId { set; get; }

        public int StarPoint { set; get; }

        public string NameCustomer { set; get; }

        public string Email { set; get; }

        public string Content { set; get; }

        public virtual ProductViewModel Product { get; set; }


    }
}
