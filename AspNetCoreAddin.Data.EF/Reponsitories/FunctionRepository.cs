using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.IReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCoreAddin.Data.EF.Reponsitories
{
    public class FunctionRepository : EFRepository<Function, string>, IFunctionRepository
    {
        private AppDbContext _context;
        public FunctionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Function> GetListFunctionWithPermission(string userId)
        {
            var query = from f in _context.Functions
                        join p in _context.Permissions on f.Id equals p.FunctionId
                        join r in _context.AppRoles on p.RoleId equals r.Id
                        join ur in _context.UserRoles on r.Id equals ur.RoleId
                        where ur.UserId.ToString() == userId && p.CanRead == true
                        select f;
            var parentIds = query.Select(x => x.ParentId).Distinct();
            query = query.Union(_context.Functions.Where(f => parentIds.Contains(f.Id)));
            return query.ToList();
        }
    }
}
