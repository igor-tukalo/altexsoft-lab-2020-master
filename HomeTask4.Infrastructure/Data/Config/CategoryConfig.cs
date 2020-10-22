using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            if (builder != null)
            {
                builder.ToTable("Categories").HasKey(x => x.Id);
                builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
                builder.HasIndex(u => u.Name).IsUnique();
                builder.Property(p => p.ParentId).IsRequired();
            }
        }
    }
}
