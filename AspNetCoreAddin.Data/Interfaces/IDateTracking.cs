using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.Interfaces
{
   public interface IDateTracking
    {
        DateTime DateCreated { get; set; }
        DateTime DateModified { set; get; }
    }
}
