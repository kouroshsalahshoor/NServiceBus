using Shared;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reciever
{
    public class CommandHandler : IHandleMessages<Command>, IHandleMessages<EventToPublish>
    {
        static ILog log = LogManager.GetLogger<CommandHandler>();

        public Task Handle(Command command, IMessageHandlerContext context)
        {
            log.Info($">>> Reciever: Received command, Id = {command.Id}");
            return Task.CompletedTask;
        }

        public Task Handle(EventToPublish @event, IMessageHandlerContext context)
        {
            log.Info($">>> Reciever: Received Event, Id = {@event.Id}");
            return Task.CompletedTask;
        }
    }
}
