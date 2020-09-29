using HomeTask4.Cmd.Navigation.WindowNavigation;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Services
{
    public class StartAppService : BackgroundService
    {
        private readonly IUnitOfWork unitOfWork;
        public StartAppService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            MainWindowNavigation mainWinNav = new MainWindowNavigation(unitOfWork);
            new ProgramMenu(mainWinNav).CallMenu();
            return Task.CompletedTask;
        }
    }
}
