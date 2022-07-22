using Grpc.Core;
using OpenTelemetry.Proto.Collector.Metrics.V1;


// New client talking to a server that returns the new protos
// Change the port to 30051 to talk to a server that returns the old protos. No message will be printed to the console
Channel channel = new Channel("127.0.0.1:30052", ChannelCredentials.Insecure);

var client = new MetricsService.MetricsServiceClient(channel);

var req = new ExportMetricsServiceRequest();

var response = client.Export(req);

var partialSuccess = response.PartialSuccess;

if (partialSuccess is not null)
{
    Console.WriteLine("Number of rejected data points: {0}", response.PartialSuccess.RejectedDataPoints);

    if (!string.IsNullOrEmpty(partialSuccess.ErrorMessage))
    {
        Console.WriteLine("Error message: {0}", partialSuccess.ErrorMessage);
    }
}

channel.ShutdownAsync().Wait();
Console.WriteLine("Press any key to exit...");
