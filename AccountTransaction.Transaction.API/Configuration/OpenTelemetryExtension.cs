using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace AccountTransaction.Transaction.API.Configuration
{
    public static class OpenTelemetryExtension
    {
        public static void AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenTelemetryTracing(telemetry =>
            {
                var resourceBuilder = ResourceBuilder
                    .CreateDefault()
                    .AddService(typeof(OpenTelemetryExtension).Assembly.GetName().Name);

                telemetry
                    .SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .SetSampler(new AlwaysOnSampler())
                    .AddJaegerExporter(jaegerOptions =>
                    {
                        jaegerOptions.AgentHost = configuration.GetSection("DistributedTracing:Jaeger:Host").Value;
                        jaegerOptions.AgentPort = int.Parse(configuration.GetSection("DistributedTracing:Jaeger:Port").Value);
                    });
            });
        }
    }
}
