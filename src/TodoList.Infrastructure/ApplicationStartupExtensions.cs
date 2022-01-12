using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure
{
    public static class ApplicationStartupExtensions
    {
        public static async Task MigrationData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TodoListDbContext>();
                context.Database.Migrate();
                // 生成种子数据
                TodoListDbContextSeed.SeedSampleDataAsync(context).Wait();
                // 更新部分种子数据以便查看审计字段
                TodoListDbContextSeed.UpdateSampleDataAsync(context).Wait();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
