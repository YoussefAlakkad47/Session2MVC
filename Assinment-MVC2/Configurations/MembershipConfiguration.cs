using Assinment_MVC2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assinment_MVC2.Configurations
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable("Memberships");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).UseIdentityColumn(1, 1);

            builder.Property(m => m.StartDate)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.EndDate).IsRequired();

            builder.Property(m => m.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.UpdatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Ignore(m => m.Status);

            builder.HasOne(m => m.Member)
                   .WithMany(member => member.Memberships)
                   .HasForeignKey(m => m.MemberId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Plan)
                   .WithMany(plan => plan.Memberships)
                   .HasForeignKey(m => m.PlanId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
