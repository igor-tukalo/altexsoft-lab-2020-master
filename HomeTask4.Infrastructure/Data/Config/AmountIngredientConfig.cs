﻿using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class AmountIngredientConfig : IEntityTypeConfiguration<AmountIngredient>
    {
        public void Configure(EntityTypeBuilder<AmountIngredient> builder)
        {
            if (builder != null)
            {
                builder.ToTable("AmountIngredients").HasKey(x => x.Id);
                builder.Property(p => p.Amount).IsRequired();
                builder.Property(p => p.Unit).IsRequired().HasMaxLength(30);
                builder.Property(p => p.RecipeId).IsRequired();
                builder.Property(p => p.IngredientId).IsRequired();
            }
        }
    }
}
