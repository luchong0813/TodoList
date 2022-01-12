using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoList.Application.Common.Interfaces;
using TodoList.Domain.Base;

namespace TodoList.Infrastructure
{
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _Logger;

        public DomainEventService(ILogger<DomainEventService> logger)
        {
            _Logger = logger;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            _Logger.LogInformation($"发布领域事件：{domainEvent.GetType().Name}");
        }
    }
}
