using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using TodoList.Application.Common;
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

        #region 查询
        // 1. 查询基础操作接口实现
        public IQueryable<T> GetAsQueryable()
            => _DbContext.Set<T>();

        public IQueryable<T> GetAsQueryable(ISpecification<T> spec)
            => ApplySpecification(spec);

        // 2. 查询数量相关接口实现
        public int Count(Expression<Func<T, bool>> condition)
            => _DbContext.Set<T>().Count(condition);

        public int Count(ISpecification<T>? spec = null)
            => null != spec ? ApplySpecification(spec).Count() : _DbContext.Set<T>().Count();

        public Task<int> CountAsync(ISpecification<T>? spec)
            => ApplySpecification(spec).CountAsync();

        // 3. 查询存在性相关接口实现
        public bool Any(ISpecification<T>? spec)
            => ApplySpecification(spec).Any();

        public bool Any(Expression<Func<T, bool>>? condition = null)
            => null != condition ? _DbContext.Set<T>().Any(condition) : _DbContext.Set<T>().Any();

        // 4. 根据条件获取原始实体类型数据相关接口实现
        public async Task<T?> GetAsync(Expression<Func<T, bool>> condition)
            => await _DbContext.Set<T>().FirstOrDefaultAsync(condition);

        public async Task<IReadOnlyList<T>> GetAsync()
            => await _DbContext.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T>? spec)
            => await ApplySpecification(spec).AsNoTracking().ToListAsync();

        // 5. 根据条件获取映射实体类型数据相关接口实现
        public TResult? SelectFirstOrDefault<TResult>(ISpecification<T>? spec, Expression<Func<T, TResult>> selector)
            => ApplySpecification(spec).AsNoTracking().Select(selector).FirstOrDefault();

        public Task<TResult?> SelectFirstOrDefaultAsync<TResult>(ISpecification<T>? spec, Expression<Func<T, TResult>> selector)
            => ApplySpecification(spec).AsNoTracking().Select(selector).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<TResult>> SelectAsync<TResult>(Expression<Func<T, TResult>> selector)
            => await _DbContext.Set<T>().AsNoTracking().Select(selector).ToListAsync();

        public async Task<IReadOnlyList<TResult>> SelectAsync<TResult>(Expression<Func<T, TResult>> selector, ISpecification<T>? spec)
            => await ApplySpecification(spec).AsNoTracking().Select(selector).ToListAsync();

        public async Task<IReadOnlyList<TResult>> SelectAsync<TGroup, TResult>(
            Expression<Func<T, TGroup>> groupExpression,
            Expression<Func<IGrouping<TGroup, T>, TResult>> selector,
            ISpecification<T>? spec = null)
            => null != spec ?
                await ApplySpecification(spec).AsNoTracking().GroupBy(groupExpression).Select(selector).ToListAsync() :
                await _DbContext.Set<T>().AsNoTracking().GroupBy(groupExpression).Select(selector).ToListAsync();

        // 用于拼接所有Specification的辅助方法，接收一个`IQuerybale<T>对象（通常是数据集合）
        // 和一个当前实体定义的Specification对象，并返回一个`IQueryable<T>`对象为子句执行后的结果。
        private IQueryable<T> ApplySpecification(ISpecification<T>? spec)
            => SpecificationEvaluator<T>.GetQuery(_DbContext.Set<T>().AsQueryable(), spec);
        #endregion
    }
}
