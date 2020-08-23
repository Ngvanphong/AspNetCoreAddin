using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.IReponsitories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AspNetCoreAddin.Data.EF.Reponsitories
{
    public class PermissionRepository : EFRepository<Permission, int>, IPermissionRepository
    {
        private AppDbContext _context;

        public PermissionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Permission> GetByUserId(Guid userId)
        {
            var query = from f in _context.Functions
                        join p in _context.Permissions on f.Id equals p.FunctionId
                        join r in _context.AppRoles on p.RoleId equals r.Id
                        join ur in _context.UserRoles on r.Id equals ur.RoleId
                        join u in _context.Users on ur.UserId equals u.Id
                        where u.Id == userId
                        select p;
            return query.ToList();
        }
    }
}
