using AspNetCoreAddin.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.Interfaces
{
   public interface ICommentService:IDisposable
    {
        void Add(CommentViewModel commentVm);

        List<CommentViewModel> GetCommentByTagPagging(string tag, int page, int pageSize, out int totalRow);

        void Delete(int id);

        void SaveChanges();


    }
}
