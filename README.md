# OTLP Partial success experimentation


This repo contains sample apps that simulates the behavior of OTLP exporters
using old/new protos talking to servers that also use old/new protos.

Made as part of
https://github.com/open-telemetry/opentelemetry-specification/issues/2454
in order to ensure backwards compatibility.


There's 2 servers - one returning the current "old" protos,
without the partial success fields and the other returning the "new" protos,
containing the new fields to be used for partial success export requests.

And there's 2 clients - one using the current "old" protos, talking to the server
that returns the new protos. The other client uses the new protos and talks
to the server using the "new" protos. 

The clients can be changed to talk to the other servers.

The client using the new protos logs the new fields to the console when they
are available.

## How to run

Start both servers:

```shell
dotnet run --project ./src/OldServer/OldServer.csproj
dotnet run --project ./src/NewServer/NewServer.csproj
```

Then run each client

```shell
dotnet run --project ./src/OldClient/OldClient.csproj
dotnet run --project ./src/NewClient/NewClient.csproj
```

## Requirements

- .NET SDK 6.x
