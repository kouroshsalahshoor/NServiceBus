
using NServiceBus.Logging;

Console.Title = "RetailSystem>Shipping";

//variables
IEndpointInstance _endpointInstance = default!;
ILog _log = LogManager.GetLogger<Program>();

await start();
Console.ReadKey();
await stop();

async Task start()
{
    var endpointConfiguration = new EndpointConfiguration("Shipping");
    // Choose JSON to serialize and deserialize messages
    endpointConfiguration.UseSerialization<SystemJsonSerializer>();
    var transport = endpointConfiguration.UseTransport<LearningTransport>();
    var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();

    _endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
}

async Task stop()
{
    await _endpointInstance.Stop().ConfigureAwait(false);
}