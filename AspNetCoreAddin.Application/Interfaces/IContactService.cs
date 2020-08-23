using AspNetCoreAddin.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.Interfaces
{
    public interface IContactService:IDisposable
    {
        ContactViewModel GetContact();

        void Add(ContactViewModel contact);

        void Update(ContactViewModel contact);

        void SaveChanges();
    }
}
