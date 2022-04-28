using Grpc.Core;
using Opentelemetry.Proto.Collector.Metrics.V1;


// New client talking to a server that returns the new protos
// Change the port to 30051 to talk to a server that returns the old protos. No message will be printed to the console
Channel channel = new Channel("127.0.0.1:30052", ChannelCredentials.Insecure);

var client = new MetricsService.MetricsServiceClient(channel);

var req = new ExportMetricsServiceRequest();

var response = client.Export(req);

if (response.Details != null)
{
    Console.WriteLine("Number of lines accepted: {0}", response.Details.AcceptedDataPoints);
    Console.WriteLine("Error message: {0}", response.Details.ErrorMessage);
}

channel.ShutdownAsync().Wait();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
