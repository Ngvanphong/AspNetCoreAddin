using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.IReponsitories
{
   public interface IPermissionRepository : IRepository<Permission, int>
    {
        List<Permission> GetByUserId(Guid userId);
    }
}
