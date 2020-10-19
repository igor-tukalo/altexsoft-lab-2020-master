using HomeTask4.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HomeTask6.Web
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
.ConfigureServices((context, services) =>
{
    services.AddInfrastructure(context.Configuration.GetConnectionString("Default"));

})
.ConfigureLogging(config =>
{
    config.ClearProviders();
})
.ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<Startup>();
});
        }
    }
}
