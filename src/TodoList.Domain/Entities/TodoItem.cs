using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoList.Domain.Base;
using TodoList.Domain.Base.Interface;
using TodoList.Domain.Enums;
using TodoList.Domain.Events;

namespace TodoList.Domain.Entities
{
    public class TodoItem : AuditableEntity, IEntity<Guid>, IHasDomainEvent
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public PriorityLevel Priority { get; set; }

        private bool _done;

        public bool Done
        {
            get { return _done; }
            set {
                if (value && _done == false) {
                    DomainEvents.Add(new TodoItemCompletedEvent(this));
                }
                _done = value;
            }
        }

        public TodoList List { get; set; } = null!;

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
