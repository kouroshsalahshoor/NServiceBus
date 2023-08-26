using Shared;
using NServiceBus.Logging;

Console.Title = "Publisher";

//variables
IEndpointInstance _endpointInstance = default!;
ILog _log = LogManager.GetLogger<Program>();
var _hasFinished = false;

await start();

while (_hasFinished == false)
{
    _log.Info("Press 'P' to publish, or 'Q' to quit.");
    var key = Console.ReadKey();
    Console.WriteLine();

    switch (key.Key)
    {
        case ConsoleKey.P:
            // Instantiate the command
            var @event = new EventToPublish
            {
                Id = Guid.NewGuid().ToString()
            };

            // Send the command to the local endpoint
            _log.Info($">>> Publisher: Publishing an Event, Id = {@event.Id}");
            await _endpointInstance.Publish(@event).ConfigureAwait(false);

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
    var endpointConfiguration = new EndpointConfiguration("Publisher");
    // Choose JSON to serialize and deserialize messages
    endpointConfiguration.UseSerialization<SystemJsonSerializer>();
    var transport = endpointConfiguration.UseTransport<LearningTransport>();
    _endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
}

async Task stop()
{
    await _endpointInstance.Stop().ConfigureAwait(false);
}