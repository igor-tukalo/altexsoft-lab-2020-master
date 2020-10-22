using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class RecipeConfig : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            if (builder != null)
            {
                builder.ToTable("Recipes").HasKey(x => x.Id);
                builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
                builder.HasIndex(u => u.Name).IsUnique();
                builder.Property(p => p.CategoryId).IsRequired();
            }
        }
    }
}
