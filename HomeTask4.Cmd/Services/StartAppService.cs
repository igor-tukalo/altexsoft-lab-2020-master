using HomeTask4.Core.Interfaces.Navigation;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Services
{
    internal class StartAppService : BackgroundService
    {
        private readonly IMainWindowNavigation _mainWindowNavigation;

        public StartAppService(IMainWindowNavigation mainWindowNavigation)
        {
            _mainWindowNavigation = mainWindowNavigation;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _mainWindowNavigation.ShowMenuAsync();
        }
    }
}
