using HomeTask4.Cmd.Navigation.ContextMenuNavigation;
using HomeTask4.Cmd.Navigation.WindowNavigation;
using HomeTask4.Cmd.Services;
using HomeTask4.Core;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using HomeTask4.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HomeTask4.Cmd
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddInfrastructure(context.Configuration.GetConnectionString("Default"));
                services.Configure<CustomSettings>(context.Configuration.GetSection("CustomSettings"));
                services.AddHostedService<StartAppService>();
                services.AddScoped<IValidationNavigation, ValidationNavigation>();
                services.AddScoped<IMainWindowNavigation, MainWindowNavigation>();
                services.AddScoped<ISettingsNavigation, SettingsNavigation>();
                services.AddScoped<ICategoriesNavigation, CategoriesNavigation>();
                services.AddScoped<IIngredientsNavigation, IngredientsNavigation>();
                services.AddScoped<ICategoriesContextMenuNavigation, CategoriesContextMenuNavigation>();
                services.AddScoped<IIngredientsContextMenuNavigation, IngredientsContextMenuNavigation>();
                services.AddScoped<IRecipesNavigation, RecipesNavigation>();
                services.AddScoped<IRecipesContextMenuNavigation, RecipesContextMenuNavigation>();
                services.AddScoped<ICookingStepsNavigation, CookingStepsNavigation>();
                services.AddScoped<ICookingStepsContextMenuNavigation, CookingStepsContextMenuNavigation>();
                services.AddScoped<IAmountRecipeIngredientsNavigation, AmountRecipeIngredientsNavigation>();
            })
            .ConfigureLogging(config =>
            {
                config.ClearProviders();
            });
        }
    }
}
