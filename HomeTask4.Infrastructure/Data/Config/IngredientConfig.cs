using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class IngredientConfig : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            if (builder != null)
            {
                builder.ToTable("Ingredients").HasKey(x => x.Id);
                builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
            }
        }
    }
}
