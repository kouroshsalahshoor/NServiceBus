using NServiceBus.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem.Sales
{
    public class CommandHandler : IHandleMessages<Order>
    {
        static ILog log = LogManager.GetLogger<CommandHandler>();
        Random _random = new Random();

        public async Task Handle(Order order, IMessageHandlerContext context)
        {
            //if (_random.Next(0, 5) == 0)
            //{
            //    throw new Exception(">>> Oops");
            //}

            log.Info($">>> Sales: Received command, Id = {order.Id}");

            await context.Publish( new OrderPlaced { OrderId = order.Id } );

            //return Task.CompletedTask;
        }
    }
}
