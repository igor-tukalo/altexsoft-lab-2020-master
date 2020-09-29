using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeTask4.Infrastructure.Data.Config
{
    class CookingStepConfig : IEntityTypeConfiguration<CookingStep>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CookingStep> builder)
        {
            if (builder != null)
            {
                builder.ToTable("CookingSteps").HasKey(x => x.Id);
                builder.Property(p => p.Name).IsRequired();
                builder.Property(p => p.Step).IsRequired();
                builder.Property(p => p.RecipeId).IsRequired();
            }
        }
    }
}
