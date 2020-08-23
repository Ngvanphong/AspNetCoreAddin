using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.Interfaces
{
   public interface IBlogService:IDisposable
    {
        int Add(BlogViewModel blogVm);

        void Update(Blog blogDb);

        void Delete(int id);

        List<BlogViewModel> GetAll();

        List<BlogViewModel> GetAllForHome(int number);

        List<BlogViewModel> GetAllPaging(string filter, int page, int pageSize, out int totalRow);

        List<BlogViewModel> GetAllPaggingByActive(int page, int pageSize, out int totalRow);

        BlogViewModel GetById(int id);

        Blog GetByIdDb(int id);

        List<TagViewModel> GetTagBlogTop(int number);

        List<BlogViewModel> GetBlogByTagPagging(string tag, int page, int pageSize, out int totalRow);

        List<TagViewModel> GetTagByBlogId(int blogId);

        void SaveChanges();
    }
}
