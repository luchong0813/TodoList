using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using TodoList.Application.Common.Interfaces;
using TodoList.Domain.Base;
using TodoList.Domain.Base.Interface;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Persistence
{
    public class TodoListDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDomainEventService _DomainEventService;

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options, IDomainEventService domainEventService) : base(options)
        {
            _DomainEventService = domainEventService;
        }

        public DbSet<Domain.Entities.TodoList> TodoLists => Set<Domain.Entities.TodoList>();
        public DbSet<TodoItem> TodoItems => Set<TodoItem>();

        public async Task<int> SaveChangeSync(CancellationToken cancellationToken)
        {
            //设置审计相关字段
            foreach (var item in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        item.Entity.CreatedBy = "Anonymous";
                        item.Entity.LastModified = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        item.Entity.CreatedBy = "Anonymous";
                        item.Entity.LastModified = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }

            //写库时发送领域事件
            var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEvents(events);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        private async Task DispatchEvents(DomainEvent[] events) {
            foreach (var item in events)
            {
                item.IsPublished = true;
                await _DomainEventService.Publish(item);
            }
        }
    }
}
