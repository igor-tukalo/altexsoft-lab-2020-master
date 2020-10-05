using HomeTask4.Core.Controllers;
using HomeTask4.Core.Interfaces;
using HomeTask4.Infrastructure.Data;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeTask4.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(connectionString));
            services.AddScoped<IRepository, EfRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIngredientsController, IngredientsController>();
            services.AddScoped<ICategoriesController, CategoriesController>();
            services.AddScoped<IRecipesController, RecipesController>();
            services.AddScoped<ICookingStepsController, CookingStepsController>();
            services.AddScoped<IAmountRecipeIngredientsController, AmountRecipeIngredientsController>();

            return services;
        }
    }
}
