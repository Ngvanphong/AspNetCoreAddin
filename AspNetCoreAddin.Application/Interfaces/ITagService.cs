using AspNetCoreAddin.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.Interfaces
{
   public interface ITagService:IDisposable
    {
        List<TagViewModel> GetAllPagging(int page, int pageSize, out int totalRows, string filter);

        void DeleteMultiNotUse();

        void SaveChanges();
    }
}
