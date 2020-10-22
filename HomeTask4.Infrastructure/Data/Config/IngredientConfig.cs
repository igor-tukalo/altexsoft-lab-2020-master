using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class IngredientConfig : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            if (builder != null)
            {
                builder.ToTable("Ingredients").HasKey(x => x.Id);
                builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
                builder.HasIndex(u => u.Name).IsUnique();
            }
        }
    }
}
