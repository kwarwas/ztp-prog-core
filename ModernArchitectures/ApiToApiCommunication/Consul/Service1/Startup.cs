using System;
using System.Threading;
using Common;
using Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Service1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseConsul("Service 1", "api");

            var healthConfiguration = new HealthConfiguration();

            var rnd = new Random();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks(healthConfiguration.HealthEndpoint);
                endpoints.MapGet("/", async context =>
                {
                    Console.WriteLine($"Request {DateTime.Now}");

                    if (rnd.Next(1, 10) < 5)
                    {
                        context.Response.StatusCode = 500;
                    }
                    else
                    {
                        await context.Response.WriteAsJsonAsync(new[]
                        {
                            new
                            {
                                Id = 1,
                                Name = "John"
                            },
                            new
                            {
                                Id = 2,
                                Name = "Alice"
                            }
                        });
                    }
                });
            });
        }
    }
}