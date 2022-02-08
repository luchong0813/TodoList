using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        ValueTask<T?> GetAsync(object key);

        Task DeleteAsync(object key);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);


        #region 查询
        //基础查询
        IQueryable<T> GetAsQueryable();
        IQueryable<T> GetAsQueryable(ISpecification<T> spec);

        //查询数量
        int Count(ISpecification<T>? spec = null);
        int Count(Expression<Func<T, bool>> condition);
        Task<int> CountAsync(ISpecification<T>? spec);

        //查询存在性
        bool Any(ISpecification<T>? spec);
        bool Any(Expression<Func<T, bool>>? condition = null);

        //根据条件获取原始实体类型数据相关接口
        Task<T?> GetAsync(Expression<Func<T, bool>> condition);
        Task<IReadOnlyList<T>> GetAsync();
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T>? spec);

        //根据条件获取映射实体类型数据相关接口，涉及到Group相关操作也在其中，使用selector来传入映射的表达式
        TResult? SelectFirstOrDefault<TResult>(ISpecification<T>? spec, Expression<Func<T, TResult>> select);
        Task<TResult?> SelectFirstOrDefaultAsync<TResult>(ISpecification<T>? spec, Expression<Func<T, TResult>> selector);
        Task<IReadOnlyList<TResult>> SelectAsync<TResult>(Expression<Func<T, TResult>> selector);
        Task<IReadOnlyList<TResult>> SelectAsync<TResult>(Expression<Func<T, TResult>> selector,ISpecification<T>? spec);
        Task<IReadOnlyList<TResult>> SelectAsync<TGroup,TResult>(Expression<Func<T, TGroup>> groupExpression, Expression<Func<IGrouping<TGroup, T>, TResult>> selector, ISpecification<T>? spec);
        #endregion

    }
}
