using HomeTask4.Core.Interfaces.Navigation;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Services
{
    internal class StartAppService : BackgroundService
    {
        private readonly IMainWindowNavigation mainWindowNavigation;

        public StartAppService(IMainWindowNavigation mainWindowNavigation)
        {
            this.mainWindowNavigation = mainWindowNavigation;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            mainWindowNavigation.ShowMenuAsync();
            return Task.CompletedTask;
        }
    }
}
