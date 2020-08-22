using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.IReponsitories
{
  public  interface ITagRepository : IRepository<Tag, string>
    {
        List<Tag> GetTagByProductId(int productId);
        List<Product> GetProductAllByTag(string tagId, int pageIndex, int pageSize, out int totalRow);

        List<Blog> GetBlogByTag(string tagId, int pageIndex, int pageSize, out int totalRow);
        List<Tag> GetTagByBlogId(int blogId);

    }
}
