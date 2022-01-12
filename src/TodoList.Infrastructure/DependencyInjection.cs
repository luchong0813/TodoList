using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoList.Application.Common.Interfaces;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<TodoListDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("SqlServerConnection"),
                    b => b.MigrationsAssembly(typeof(TodoListDbContext).Assembly.FullName));
            });
            //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<TodoListDbContext>());
            services.AddScoped<IDomainEventService, DomainEventService>();

            return services;
        }
    }
}
