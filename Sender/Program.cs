using Commands;
using NServiceBus.Logging;

Console.Title = "Sender";

//variables
IEndpointInstance _endpointInstance = default!;
ILog _log = LogManager.GetLogger<Program>();
var _hasFinished = false;

await start();

while (_hasFinished == false)
{
    _log.Info("Press 'S' to Send Command Locally (To Self), or 'Q' to quit.");
    var key = Console.ReadKey();
    Console.WriteLine();

    switch (key.Key)
    {
        case ConsoleKey.S:
            // Instantiate the command
            var command = new Command
            {
                Id = Guid.NewGuid().ToString()
            };

            // Send the command to the local endpoint
            _log.Info($">>> Sender: Sending command, Id = {command.Id}");
            await _endpointInstance.Send("Reciever1", command).ConfigureAwait(false);
            //await _endpointInstance.Send(command).ConfigureAwait(false);

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
    var endpointConfiguration = new EndpointConfiguration("Sender");
    // Choose JSON to serialize and deserialize messages
    endpointConfiguration.UseSerialization<SystemJsonSerializer>();
    var transport = endpointConfiguration.UseTransport<LearningTransport>();

    //var routing = transport.Routing();
    //routing.RouteToEndpoint(typeof(Command), "Reciever1");

    _endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
}

async Task stop()
{
    await _endpointInstance.Stop().ConfigureAwait(false);
}