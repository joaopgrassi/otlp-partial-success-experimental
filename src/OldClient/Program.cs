using Grpc.Core;
using Opentelemetry.Proto.Collector.Metrics.V1;


// Old client talking to a server that returns the new protos
Channel channel = new Channel("127.0.0.1:30052", ChannelCredentials.Insecure);

var client = new MetricsService.MetricsServiceClient(channel);

var req = new ExportMetricsServiceRequest();

var response = client.Export(req);

channel.ShutdownAsync().Wait();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
