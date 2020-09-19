using HomeTask4.Cmd.Navigation.WindowNavigation;
using HomeTask4.Core.Entities;
using HomeTask4.Infrastructure.Extensions;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

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
        private static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddInfrastructure();
                })
                .ConfigureLogging(config =>
                {
                    config.ClearProviders();
                    config.AddConsole();
                })
                .Build();

            ILogger<Program> logger = host.Services.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Hello World!");

            logger.LogDebug("Trying to get repository...");
            IRepository repository = host.Services.GetRequiredService<IRepository>();

            try
            {
                logger.LogDebug("Trying to get temp entity...");
                System.Threading.Tasks.Task<TempEntity> entity = repository.GetByIdAsync<TempEntity>(1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred durign getting temp entity");
            }

            logger.LogInformation("Good bye!");

            Console.WindowHeight = Console.LargestWindowHeight;
            MainWindowNavigation mainWindowNav = new MainWindowNavigation();
            try
            {
                ProgramMenu programMenu = new ProgramMenu(mainWindowNav);
                programMenu.CallMenu();
            }
            finally
            {
                mainWindowNav.Dispose();
            }
        }
    }
}
