using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HomeTask6.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.Configure<CustomSettings>(Configuration.GetSection("CustomSettings"));
            //services.AddScoped<IValidationNavigation, ValidationNavigation>();
            //services.AddScoped<IMainWindowNavigation, MainWindowNavigation>();
            //services.AddScoped<ISettingsNavigation, SettingsNavigation>();
            //services.AddScoped<ICategoriesNavigation, CategoriesNavigation>();
            //services.AddScoped<IIngredientsNavigation, IngredientsNavigation>();
            //services.AddScoped<ICategoriesContextMenuNavigation, CategoriesContextMenuNavigation>();
            //services.AddScoped<IIngredientsContextMenuNavigation, IngredientsContextMenuNavigation>();
            //services.AddScoped<IRecipesNavigation, RecipesNavigation>();
            //services.AddScoped<IRecipesContextMenuNavigation, RecipesContextMenuNavigation>();
            //services.AddScoped<ICookingStepsNavigation, CookingStepsNavigation>();
            //services.AddScoped<ICookingStepsContextMenuNavigation, CookingStepsContextMenuNavigation>();
            //services.AddScoped<IAmountRecipeIngredientsNavigation, AmountRecipeIngredientsNavigation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
