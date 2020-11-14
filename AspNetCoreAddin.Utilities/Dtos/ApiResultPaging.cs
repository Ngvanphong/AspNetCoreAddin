using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Utilities.Dtos
{
    public class ApiResultPaging<T>
    {
        public List<T> Items { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public int TotalRows { set; get; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((double)(TotalRows / PageSize)); }
        }
    }
}
