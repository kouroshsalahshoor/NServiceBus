﻿using Commands;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendToSelf
{
    public class MessageHandler : IHandleMessages<Command>
    {
        static ILog log = LogManager.GetLogger<MessageHandler>();

        public Task Handle(Command command, IMessageHandlerContext context)
        {
            log.Info($"Received command, Id = {command.Id}");
            return Task.CompletedTask;
        }
    }
}