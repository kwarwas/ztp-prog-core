using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Common
{
    public static class ServiceDiscoveryExtensions
    {
        private static readonly ConsulHelper ConsultHelper = new();

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, string name, string tag)
        {
            return app.UseConsul(name, new[] {tag});
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, string name, string[]? tags)
        {
            app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() =>
            {
                ConsultHelper.Register(app, name, tags).Wait();
            });

            return app;
        }

        public static string? GetServiceUrl(this string serviceName)
        {
            string GenerateUrl(AgentService agentService) => $"{agentService.Address}:{agentService.Port}";
            
            var service = ConsultHelper.GetServiceConfiguration(serviceName);

            return service is not null ? GenerateUrl(service) : null;
        }
    }
}