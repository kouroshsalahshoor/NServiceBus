
Console.Title = "Reciever";

//variables
IEndpointInstance _endpointInstance = default!;

await start();

await stop();

async Task start()
{
    var endpointConfiguration = new EndpointConfiguration("Reciever");
    // Choose JSON to serialize and deserialize messages
    endpointConfiguration.UseSerialization<SystemJsonSerializer>();
    var transport = endpointConfiguration.UseTransport<LearningTransport>();
    _endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
}

async Task stop()
{
    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();

    await _endpointInstance.Stop().ConfigureAwait(false);
}