using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.Interfaces
{
  public  interface ISlideService:IDisposable
    {
        List<SlideViewModel> GetAllPagging(int page, int pageSize, string filter, out int totalRow);

        List<SlideViewModel> GetAll();

        SlideViewModel GetById(int id);

        Slide GetByIdDb(int id);

        void Delete(int id);

        void Update(Slide slideDb);

        void Add(SlideViewModel slideVm);

        void SaveChanges();

    }
}
