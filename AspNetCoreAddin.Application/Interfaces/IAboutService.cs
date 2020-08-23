using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.Interfaces
{
   public interface IAboutService:IDisposable
    {
        AboutViewModel GetAbout();

        void Add(About contact);

        void Update(About contact);


        void SaveChanges();
    }
}
