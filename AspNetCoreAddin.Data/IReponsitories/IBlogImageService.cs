using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.IReponsitories
{
   public interface IBlogImageService:IDisposable
    {
        void Add(BlogImageViewModel blogImage);

        void Update(BlogImage blogImageDb);

        BlogImageViewModel GetById(int id);

        BlogImage GetByIdDb(int id);

        List<BlogImageViewModel> GetAllByBlogId(int blogId);

        void Delete(int id);

        void SaveChanges();
    }
}
