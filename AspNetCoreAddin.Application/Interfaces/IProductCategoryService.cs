using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.Interfaces
{
  public  interface IProductCategoryService:IDisposable
    {
        ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm);

        void Update(ProductCategoryViewModel productCategoryVm);

        void UpdateDb(ProductCategory productCategory);

        void Delete(int id);

        List<ProductCategoryViewModel> GetAll();

        List<ProductCategoryViewModel> GetAll(string keyword);

        ProductCategoryViewModel GetById(int id);

        ProductCategory GetByIdDb(int id);


        List<ProductCategoryViewModel> GetHomeCategories(int top);

        List<ProductCategoryViewModel> GetCategoryFooter(int top);

        void SaveChanges();
    }
}
