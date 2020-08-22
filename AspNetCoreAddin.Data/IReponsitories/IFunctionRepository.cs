using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.IReponsitories
{
   public interface IFunctionRepository : IRepository<Function, string>
    {
        List<Function> GetListFunctionWithPermission(string userId);
    }
}
