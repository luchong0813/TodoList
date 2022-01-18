using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
