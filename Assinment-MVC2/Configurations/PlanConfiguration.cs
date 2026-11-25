using Assinment_MVC2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assinment_MVC2.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("Plans", table =>
            {
                table.HasCheckConstraint("CK_Plan_DurationDays", "[DurationDays] BETWEEN 1 AND 365");
            });

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn(1, 1);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired();

            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.IsActive).IsRequired();

            builder.Property(p => p.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.UpdatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();
        }
    }
}
