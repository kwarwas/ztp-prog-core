using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Configuration;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace Common
{
    class ConsulHelper
    {
        public async Task Register(IApplicationBuilder app, string name, string[]? tags)
        {
            var serviceProvider = app.ApplicationServices;

            var logger = serviceProvider.GetRequiredService<ILogger<ConsulHelper>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var healthConfiguration = configuration.GetSection("Health")
                .Get<HealthConfiguration>() ?? new HealthConfiguration();
            var proxyConfiguration = configuration.GetSection("Proxy")
                .Get<ProxyConfiguration>() ?? new ProxyConfiguration();

            var features = app.Properties["server.Features"] as FeatureCollection;
            var addresses = features?.Get<IServerAddressesFeature>();
            var address = addresses?.Addresses.FirstOrDefault();

            if (address is null)
            {
                logger.LogError("Incorrect web app URL address");
                return;
            }

            var uri = new Uri(address);
            using var client = new ConsulClient();

            var agentRegistration = new AgentServiceRegistration
            {
                Address = $"{uri.Scheme}://{uri.Host}",
                Port = uri.Port,
                ID = Assembly.GetEntryAssembly()?.GetName().Name,
                Name = name,
                Tags = tags
            };

            if (healthConfiguration.UseHealthCheck)
            {
                var httpRoot = string.IsNullOrWhiteSpace(proxyConfiguration.Url)
                    ? address
                    : $"{proxyConfiguration.Url}:{uri.Port}";

                agentRegistration.Check = new AgentCheckRegistration
                {
                    HTTP = $"{httpRoot}{healthConfiguration.HealthEndpoint}",
                    Timeout = healthConfiguration.Timeout,
                    Interval = healthConfiguration.Interval,
                    DeregisterCriticalServiceAfter = healthConfiguration.DeregisterServiceAfter
                };
            }

            try
            {
                await client.Agent.ServiceRegister(agentRegistration);

                logger.LogInformation("Service {Name} {Address} has been registered in Consul",
                    agentRegistration.Name, address);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Service {Name} {Address} has NOT been registered in Consul",
                    agentRegistration.Name, address);
            }
        }

        public AgentService? GetServiceConfiguration(string serviceName)
        {
            try
            {
                return Policy
                    .HandleResult<AgentService?>(x => x is null)
                    .WaitAndRetry(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    })
                    .Execute(() =>
                    {
                        using var consulClient = new ConsulClient();
                        return consulClient.Agent.Services()
                            .Result
                            .Response
                            .SingleOrDefault(x => x.Value.Service == serviceName).Value;
                    });
            }
            catch
            {
                return null;
            }
        }
    }
}