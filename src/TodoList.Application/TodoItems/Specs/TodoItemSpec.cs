using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoList.Application.Common;
using TodoList.Domain.Entities;
using TodoList.Domain.Enums;

namespace TodoList.Application.TodoItems.Specs
{
    public sealed class TodoItemSpec: SpecificationBase<TodoItem>
    {
        public TodoItemSpec(bool done, PriorityLevel priority):base(t=>t.Done==done&&t.Priority==priority)
        {

        }
    }
}
