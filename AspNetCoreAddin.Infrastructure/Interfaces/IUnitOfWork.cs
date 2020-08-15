using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Infrastructure.Interfaces
{
   public interface IUnitOfWork:IDisposable
    {
        void Commit();
    }
}
