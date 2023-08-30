using NServiceBus.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem.Shipping
{
    public class OrderPlacedHandler : Saga<OrderPlacedHandler.OrderPlacedData>,
        IAmStartedByMessages<OrderPlaced>, IHandleMessages<OrderBilled>, IHandleMessages<OrderShipped>
        //IHandleMessages<OrderPlaced>, IHandleMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();
        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            Data.Placed = true;
            log.Info($">>> RetailSystem.Shipping>OrderPlacedHandler>OrderPlaced: OrderId = {Data.OrderId}, Placed = {Data.Placed}, Billed = {Data.Billed}");
            return ProcessOrder(context);
            //return Task.CompletedTask;
        }

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            Data.Billed = true;
            log.Info($">>> RetailSystem.Shipping>OrderPlacedHandler>OrderBilled: OrderId = {Data.OrderId}, Placed = {Data.Placed}, Billed = {Data.Billed}");
            return ProcessOrder(context);
            //return Task.CompletedTask;
        }

        public Task Handle(OrderShipped message, IMessageHandlerContext context)
        {
            log.Info($"Order [{message.OrderId}] - Successfully shipped.");
            return Task.CompletedTask;
        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (Data.Placed && Data.Billed)
            {
                await context.SendLocal(new OrderShipped() { OrderId = Data.OrderId });
                MarkAsComplete();
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderPlacedData> mapper)
        {
            mapper.MapSaga(x => x.OrderId)
                .ToMessage<OrderPlaced>(y => y.OrderId)
                .ToMessage<OrderBilled>(z => z.OrderId);
        }

        public class OrderPlacedData : ContainSagaData
        {
            public string OrderId { get; set; } = string.Empty;
            public bool Placed { get; set; }
            public bool Billed { get; set; }
        }
    }

    //public class OrderPlacedHandler : IHandleMessages<OrderPlaced>, IHandleMessages<OrderBilled>
    //{
    //    static ILog log = LogManager.GetLogger<OrderPlacedHandler>();
    //    public Task Handle(OrderPlaced message, IMessageHandlerContext context)
    //    {
    //        log.Info($">>> RetailSystem.Shipping>OrderPlacedHandler: Received message, OrderPlaced>OrderId = {message.OrderId}");
    //        return Task.CompletedTask;
    //    }

    //    public Task Handle(OrderBilled message, IMessageHandlerContext context)
    //    {
    //        log.Info($">>> RetailSystem.Shipping>OrderPlacedHandler: Received message, OrderBilled>OrderId = {message.OrderId}");
    //        return Task.CompletedTask;
    //    }
    //}
}
