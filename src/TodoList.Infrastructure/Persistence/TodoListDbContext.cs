using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoList.Application.Common.Interfaces;

namespace TodoList.Infrastructure.Persistence
{
    public class TodoListDbContext : DbContext, IApplicationDbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options):base(options)
        {

        }

        public Task<int> SaveChangeSync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
