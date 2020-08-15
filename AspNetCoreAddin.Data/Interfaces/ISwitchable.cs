using AspNetCoreAddin.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.Interfaces
{
  public  interface ISwitchable
    {
        Status Status { get; set; }
    }
}
