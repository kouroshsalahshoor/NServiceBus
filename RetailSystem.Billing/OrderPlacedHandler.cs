using NServiceBus.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem.Billing
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();
        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($">>> RetailSystem.Billing>OrderPlacedHandler: Received message, OrderId = {message.OrderId}");

            await context.Publish(new OrderBilled { OrderId = message.OrderId });
        }
    }
}
