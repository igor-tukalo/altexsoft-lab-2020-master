
using HomeTask4.Cmd.Navigation.ContextMenuNavigation;
using HomeTask4.Cmd.Navigation.WindowNavigation;
using HomeTask4.Cmd.Services;
using HomeTask4.Core;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeTask4.Cmd
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //// inject a BackgroundService
            services.AddHostedService<StartAppService>();
            services.AddScoped<IMainWindowNavigation, MainWindowNavigation>();
            services.AddScoped<ISettingsNavigation, SettingsNavigation>();
            services.AddScoped<ICategoriesNavigation, CategoriesNavigation>();
            services.AddScoped<IIngredientsNavigation, IngredientsNavigation>();
            services.AddScoped<ICategoriesContextMenuNavigation, CategoriesContextMenuNavigation>();
            services.AddScoped<IIngredientsContextMenuNavigation, IngredientsContextMenuNavigation>();
            services.AddScoped<IRecipesNavigation, RecipesNavigation>();
            services.AddScoped<IRecipesContextMenuNavigation, RecipesContextMenuNavigation>();
            services.AddScoped<IValidationNavigation, ValidationNavigation>();


            services.Configure<CustomSettings>(Configuration.GetSection("CustomSettings"));
            // add in an IHttpClientFactory
            services.AddHttpClient();

            // add in IMemoryCache
            services.AddMemoryCache();
        }
    }
}
