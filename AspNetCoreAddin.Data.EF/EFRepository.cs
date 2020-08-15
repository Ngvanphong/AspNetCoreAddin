using AspNetCoreAddin.Infrastructure.Interfaces;
using AspNetCoreAddin.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.EF
{
    public class EFRepository<T, K> : IRepository<T, K>, IDisposable where T : DomainEntity<K>
    {
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public System.Linq.IQueryable<T> FindAll(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public System.Linq.IQueryable<T> FindAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public T FindById(K id, params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public T FindSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(K id)
        {
            throw new NotImplementedException();
        }

        public void RemoveMultiple(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
