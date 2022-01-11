using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoList.Domain.Base;
using TodoList.Domain.Base.Interface;
using TodoList.Domain.ValueObjects;

namespace TodoList.Domain.Entities
{
    public class TodoList : AuditableEntity, IEntity<Guid>, IHasDomainEvent, IAggregateRoot
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public Colour Colour { get; set; } = Colour.White;

        public List<TodoItem> Items { get; private set; } = new List<TodoItem>();
        public List<DomainEvent> DomainEvents { get; set; }=new List<DomainEvent>();
    }
}
