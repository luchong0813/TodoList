using Microsoft.EntityFrameworkCore.Query;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.Common.Interfaces
{
    public interface ISpecification<T>
    {
        //查询条件子句
        Expression<Func<T, bool>> Criteria { get; }
        //Include子句
        Func<IQueryable<T>, IIncludableQueryable<T, object>> Include { get; }
        //OrderBy子句
        Expression<Func<T, object>> OrderBy { get; }
        //OrderByDescending子句
        Expression<Func<T, Object>> OrderByDescending { get; }
        
        //分页属性
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
