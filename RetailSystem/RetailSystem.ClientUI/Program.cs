using Shared;
using NServiceBus.Logging;
using NServiceBus;

Console.Title = "RetailSystem>ClientUI";

//variables
IEndpointInstance _endpointInstance = default!;
ILog _log = LogManager.GetLogger<Program>();
var _hasFinished = false;

await start();

while (_hasFinished == false)
{
    _log.Info("Press 'S' to Send, or 'Q' to quit.");
    var key = Console.ReadKey();
    Console.WriteLine();

    switch (key.Key)
    {
        case ConsoleKey.S:            

            var order = new Order { Id = Guid.NewGuid().ToString() };
            await _endpointInstance.Send("Sales", order);
            _log.Info($">>> ClientUI: Sending Order, Id = {order.Id}");
            break;

        case ConsoleKey.Q:
            _hasFinished = true;
            break;

        default:
            _log.Info("Unknown input. Please try again.");
            break;
    }
}

await stop();

async Task start()
{
    var endpointConfiguration = new EndpointConfiguration("ClientUI");
    // Choose JSON to serialize and deserialize messages
    endpointConfiguration.UseSerialization<SystemJsonSerializer>();
    endpointConfiguration.SendOnly();

    var transport = endpointConfiguration.UseTransport<LearningTransport>();
    var routing = transport.Routing();
    routing.RouteToEndpoint(typeof(Command), "Sales");

    _endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
}

async Task stop()
{
    await _endpointInstance.Stop().ConfigureAwait(false);
}