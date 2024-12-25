using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Infrastructure.Logging
{
    public static class SerilogConfiguration
    {
        public static void ConfigureSerilog(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("UserDatabase"),
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = true
                    })
                .CreateLogger();
        }
    }
}
