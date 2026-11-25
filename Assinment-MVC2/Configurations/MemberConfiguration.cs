using Assinment_MVC2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assinment_MVC2.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members", table =>
            {
                table.HasCheckConstraint(
                    "CK_Member_Phone",
                    "[Phone] LIKE '010________' OR [Phone] LIKE '011________' OR [Phone] LIKE '012________' OR [Phone] LIKE '015________'");

                table.HasCheckConstraint(
                    "CK_Member_Email",
                    "[Email] LIKE '%_@_%._%' AND [Email] NOT LIKE '% %'");
            });

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).UseIdentityColumn(1, 1);

            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(m => m.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(m => m.Email).IsUnique();

            builder.Property(m => m.Phone)
                   .IsRequired()
                   .HasMaxLength(11);

            builder.HasIndex(m => m.Phone).IsUnique();

            builder.Property(m => m.DateOfBirth)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(m => m.Gender).IsRequired();

            builder.Property(m => m.JoinDate)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.Photo);

            builder.Property(m => m.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.UpdatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.OwnsOne(m => m.Address, address =>
            {
                address.Property(a => a.BuildingNumber)
                       .HasColumnName("Address_BuildingNumber")
                       .IsRequired();

                address.Property(a => a.Street)
                       .HasColumnName("Address_Street")
                       .IsRequired();

                address.Property(a => a.City)
                       .HasColumnName("Address_City")
                       .IsRequired();
            });

            builder.Navigation(m => m.Address).IsRequired();
        }
    }
}
