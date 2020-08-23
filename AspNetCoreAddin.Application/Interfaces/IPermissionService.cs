using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.Interfaces
{
   public interface IPermissionService:IDisposable
    {
        List<PermissionViewModel> GetByFunctionId(string functionId);

        List<PermissionViewModel> GetByUserId(Guid userId);

        void Add(PermissionViewModel permission);

        void AddDb(Permission permission);

        void DeleteAll(string functionId);

        void DeleteAllByRoleId(string roleId);

        void SaveChanges();
    }
}
