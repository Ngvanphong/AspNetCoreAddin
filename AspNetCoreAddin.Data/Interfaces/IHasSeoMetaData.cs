using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.Interfaces
{
    public interface IHasSeoMetaData
    {
        string SeoPageTitle { get; set; }
        string SeoAlias { get; set; }
        string SeoKeywords { set; get; }
        string SeoDescription { get; set; }
    }
}
