using Assinment_MVC2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assinment_MVC2.Configurations
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.ToTable("Trainers", table =>
            {
                table.HasCheckConstraint(
                    "CK_Trainer_Phone",
                    "[Phone] LIKE '010________' OR [Phone] LIKE '011________' OR [Phone] LIKE '012________' OR [Phone] LIKE '015________'");

                table.HasCheckConstraint(
                    "CK_Trainer_Email",
                    "[Email] LIKE '%_@_%._%' AND [Email] NOT LIKE '% %'");
            });

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn(1, 1);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(t => t.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(t => t.Email).IsUnique();

            builder.Property(t => t.Phone)
                   .IsRequired()
                   .HasMaxLength(11);

            builder.HasIndex(t => t.Phone).IsUnique();

            builder.Property(t => t.DateOfBirth)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(t => t.Gender).IsRequired();

            builder.Property(t => t.HireDate)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.Specialties).IsRequired();

            builder.Property(t => t.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.UpdatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.OwnsOne(t => t.Address, address =>
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

            builder.Navigation(t => t.Address).IsRequired();
        }
    }
}
