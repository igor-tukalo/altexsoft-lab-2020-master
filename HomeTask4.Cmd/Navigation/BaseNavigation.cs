using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Infrastructure.Extensions;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace HomeTask4.Cmd.Navigation
{
    public abstract class BaseNavigation : INavigation
    {
        internal IUnitOfWork UnitOfWork { get; set; }
        protected IHost HostBuild { get; set; }

        internal BaseNavigation()
        {
            HostBuild = CreateHostBuilder(false).Build();
            UnitOfWork = HostBuild.Services.GetRequiredService<IUnitOfWork>();
        }

        public List<EntityMenu> ItemsMenu { get; set; }
        public virtual void CallNavigation()
        {
            new Navigation().CallNavigation(ItemsMenu, SelectMethodMenu);
        }
        public virtual void SelectMethodMenu(int id) { }

        /// <summary>
        /// This method should be separate to support EF command-line tools in design time
        /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
        /// </summary>
        /// <param name="args"></param>
        /// <returns><see cref="IHostBuilder" /> hostBuilder</returns>
        protected static IHostBuilder CreateHostBuilder(bool isConsole)
        {
            return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddInfrastructure(context.Configuration.GetConnectionString("Default"));
            })
            .ConfigureLogging(config =>
            {
                config.ClearProviders();
                if (isConsole)
                {
                    config.AddConsole();
                }
            });
        }
    }
}
