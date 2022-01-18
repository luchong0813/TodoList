using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoList.Application.Common.Interfaces;

namespace TodoList.Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly TodoListDbContext _DbContext;

        public RepositoryBase(TodoListDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _DbContext.Set<T>().AddAsync(entity, cancellationToken);
            await _DbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(object key)
        {
            var entity = await GetAsync(key);
            if (entity is not null)
            {
                await DeleteAsync(entity);
            }
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _DbContext.Set<T>().Remove(entity);
            await _DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _DbContext.Set<T>().RemoveRange(entities);
            await _DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual ValueTask<T?> GetAsync(object key)
        {
            return _DbContext.Set<T>().FindAsync(key);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _DbContext.Entry(entity).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
