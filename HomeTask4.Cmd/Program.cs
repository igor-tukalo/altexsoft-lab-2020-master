using HomeTask4.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

/// <summary>
/// A skeleton for the Home Task 4 in AltexSoft Lab 2020
/// For more details how to organize configuration, logging and dependency injections in console app
/// watch https://www.youtube.com/watch?v=GAOCe-2nXqc
///
/// For more information about General Host
/// read https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1
///
/// For more information about Logging
/// read https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1
///
/// For more information about Dependency Injection
/// read https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1
/// </summary>
namespace HomeTask4.Cmd
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        /// <summary>
        /// This method should be separate to support EF command-line tools in design time
        /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
        /// </summary>
        /// <param name = "args" ></ param >
        /// < returns >< see cref="IHostBuilder" /> hostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddInfrastructure(context.Configuration.GetConnectionString("Default"));

            })
            .ConfigureLogging(config =>
            {
                config.ClearProviders();
            })
            .UseStartup<Startup>();
        }
    }
}
