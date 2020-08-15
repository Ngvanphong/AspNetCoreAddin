
using AspNetCoreAddin.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.EF
{
   public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {

    }
}
