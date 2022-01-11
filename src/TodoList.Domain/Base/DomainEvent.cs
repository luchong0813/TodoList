using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain.Base
{
    public abstract class DomainEvent
    {
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; set; } = DateTime.UtcNow;

        public DomainEvent()
        {
            DateOccurred= DateTimeOffset.UtcNow;
        }
    }
}
