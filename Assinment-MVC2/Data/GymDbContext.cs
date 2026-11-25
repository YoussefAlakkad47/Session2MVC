using Assinment_MVC2.Configurations;
using Assinment_MVC2.Models;
using Microsoft.EntityFrameworkCore;

namespace Assinment_MVC2.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Membership> Memberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
            modelBuilder.ApplyConfiguration(new TrainerConfiguration());
            modelBuilder.ApplyConfiguration(new PlanConfiguration());
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
            modelBuilder.ApplyConfiguration(new MembershipConfiguration());
        }
    }
}
