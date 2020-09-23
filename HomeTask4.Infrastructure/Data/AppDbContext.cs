using HomeTask4.Core.Entities;
using HomeTask4.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace HomeTask4.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AmountIngredient> AmountIngredients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CookingStep> CookingSteps { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

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
                modelBuilder.ApplyConfiguration(new TempEntityConfig());
            }
        }
    }
}
