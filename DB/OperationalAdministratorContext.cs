using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class OperationalAdministratorContext : DbContext
    {
        public OperationalAdministratorContext()
        {
        }

        public OperationalAdministratorContext (DbContextOptions<OperationalAdministratorContext> options) : base (options) 
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                   .HasMany(e => e.Members)
                   .WithOne(e => e.Team)
                   .HasForeignKey(e => e.TeamId)
                   .HasPrincipalKey(e => e.TeamId);

            modelBuilder.Entity<Account>()
                    .HasOne(e => e.Team)
                    .WithOne(e => e.Account)
                    .HasPrincipalKey<Team>(e => e.TeamId)
                    .IsRequired();
        }
    }
}