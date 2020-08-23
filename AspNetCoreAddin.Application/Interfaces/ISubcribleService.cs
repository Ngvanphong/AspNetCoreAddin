using AspNetCoreAddin.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.Interfaces
{
  public  interface ISubcribleService:IDisposable
    {
        List<SubcribleViewModel> GetPaging(int page, int pageSize, out int totalRow);

        void Add(string email);

        void SaveChanges();

        bool CheckExit(string email);

        List<SubcribleViewModel> GetAll();

        List<string> GetAllEmail();

        void Delete(int id);
    }
}
