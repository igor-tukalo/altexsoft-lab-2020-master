using HomeTask4.Core.Entities;
using HomeTask4.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace HomeTask4.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (modelBuilder != null)
            {
                modelBuilder.ApplyConfiguration(new AmountIngredientConfig());
                modelBuilder.ApplyConfiguration(new CategoryConfig());
                modelBuilder.ApplyConfiguration(new CookingStepConfig());
                modelBuilder.ApplyConfiguration(new IngredientConfig());
                modelBuilder.ApplyConfiguration(new RecipeConfig());
            }
        }
    }
}
