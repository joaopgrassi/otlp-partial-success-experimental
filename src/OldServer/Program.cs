using Grpc.Core;
using Opentelemetry.Proto.Collector.Metrics.V1;

const int Port = 30051;

Server server = new Server
{
    Services = { MetricsService.BindService(new MetricsServiceImpl()) },
    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
};
server.Start();

Console.WriteLine("Old OTLP server listening on port " + Port);
Console.WriteLine("Press any key to stop the server...");
Console.ReadKey();

server.ShutdownAsync().Wait();

class MetricsServiceImpl : MetricsService.MetricsServiceBase
{
    public override Task<ExportMetricsServiceResponse> Export(ExportMetricsServiceRequest request, ServerCallContext context)
    {
        // server that uses the old proto, without the "partial success" extra info on the response
        var resp = new ExportMetricsServiceResponse();
        return Task.FromResult(resp);
    }
}
