using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingBlocks.CQRS.Events;
using BuildingBlocks.Exception;
using BuildingBlocks.Types;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using University.Cources.Application.Services;
using University.Cources.Infrastructure.EfCore;

namespace University.Cources.Infrastructure.Services
{
    public class MessageBroker : IMessageBroker
    {
        private readonly ICapPublisher _capPublisher;
        private readonly ILogger<MessageBroker> _logger;
        private readonly CourseDbContext _studentDbContext;
        private readonly OutboxOptions _outbox;
        private readonly IExceptionToMessageMapper _exceptionToMessageMapper;


        public MessageBroker(ICapPublisher capPublisher, ILogger<MessageBroker> logger, CourseDbContext studentDbContext, OutboxOptions outbox, IExceptionToMessageMapper exceptionToMessageMapper)
        {
            _capPublisher = capPublisher;
            _logger = logger;
            _studentDbContext = studentDbContext;
            _outbox = outbox;
            _exceptionToMessageMapper = exceptionToMessageMapper;
        }
        
        public async Task PublishAsync(IEnumerable<IEvent> events)
        {
            if (events is null)
            {
                return;
            }
            
            foreach (var @event in events)
            {
                if (@event is null)
                {
                    continue;
                }
                
                if (_outbox.Enabled)
                {
                    using (var trans = _studentDbContext.Database.BeginTransaction(_capPublisher, autoCommit: true))
                    {
                        await _capPublisher.PublishAsync(@event.GetType().Name, @event);
                    }
                    continue;
                }
                
                await _capPublisher.PublishAsync(@event.GetType().Name, @event);
            }
        }
    }
}