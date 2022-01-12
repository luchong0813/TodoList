using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoList.Domain.Entities;
using TodoList.Domain.Enums;
using TodoList.Domain.ValueObjects;

namespace TodoList.Infrastructure.Persistence
{
    public static class TodoListDbContextSeed
    {
        public static async Task SeedSampleDataAsync(TodoListDbContext context) {
            //如果该实体不包含任何数据
            if (!context.TodoLists.Any()) {
                var list = new Domain.Entities.TodoList {
                    Title = "超市购物",
                    Colour=Colour.Orange
                };
                list.Items.Add(new TodoItem { Title = "猪肉", Done = true, Priority = PriorityLevel.High });
                list.Items.Add(new TodoItem { Title = "玉米", Done = true, Priority = PriorityLevel.Medium });
                list.Items.Add(new TodoItem { Title = "鸡蛋", Done = true, Priority = PriorityLevel.None });
                list.Items.Add(new TodoItem { Title = "晨光酸牛奶" });
                list.Items.Add(new TodoItem { Title = "葱姜蒜" });
                list.Items.Add(new TodoItem { Title = "车厘子" });
                list.Items.Add(new TodoItem { Title = "韭菜" });
                list.Items.Add(new TodoItem { Title = "乌鸡" });

                context.TodoLists.Add(list);
                await context.SaveChangesAsync();
            }
        }

        public static async Task UpdateSampleDataAsync(TodoListDbContext context) {
            var todoList = await context.TodoLists.FirstOrDefaultAsync();
            if (todoList == null) return;
            todoList.Title = "购物-修改过的";

            context.Update(todoList);
            await context.SaveChangesAsync();
        }
    }
}
