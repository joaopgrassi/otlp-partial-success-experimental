using Grpc.Core;
using Opentelemetry.Proto.Collector.Metrics.V1;

const int Port = 30052;

Server server = new Server
{
    Services = { MetricsService.BindService(new MetricsServiceImpl()) },
    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
};
server.Start();

Console.WriteLine("New OTLP server listening on port " + Port);
Console.WriteLine("Press any key to stop the server...");
Console.ReadKey();

server.ShutdownAsync().Wait();

class MetricsServiceImpl : MetricsService.MetricsServiceBase
{
    public override Task<ExportMetricsServiceResponse> Export(ExportMetricsServiceRequest request, ServerCallContext context)
    {
        var resp = new ExportMetricsServiceResponse
        {
            // server sending the new "partial success" extra info
            Details = new ExportMetricsPartialSuccess
            {
                AcceptedDataPoints = 3,
                ErrorMessage = "Some error happened, here are the details..."
            }
        };

        return Task.FromResult(resp);
    }
}
