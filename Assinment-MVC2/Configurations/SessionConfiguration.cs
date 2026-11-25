using Assinment_MVC2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assinment_MVC2.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions", table =>
            {
                table.HasCheckConstraint("CK_Session_Capacity", "[Capacity] BETWEEN 1 AND 25");
                table.HasCheckConstraint("CK_Session_EndDate", "[EndDate] > [StartDate]");
            });

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).UseIdentityColumn(1, 1);

            builder.Property(s => s.Description).IsRequired();

            builder.Property(s => s.StartDate).IsRequired();
            builder.Property(s => s.EndDate).IsRequired();

            builder.Property(s => s.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Property(s => s.UpdatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.HasOne(s => s.Trainer)
                   .WithMany(t => t.Sessions)
                   .HasForeignKey(s => s.TrainerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
