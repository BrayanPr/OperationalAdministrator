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

        public DbSet<Team> Teams { get; set; }

        public DbSet<History> TeamHistory { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                   .HasMany(e => e.Members)
                   .WithOne(e => e.Team)
                   .HasForeignKey(e => e.TeamId)
                   .HasPrincipalKey(e => e.TeamId);

            modelBuilder.Entity<Team>()
                 .HasMany(e => e.Accounts)
                 .WithOne(e => e.Team)
                 .HasForeignKey(e => e.TeamId)
                 .HasPrincipalKey(e => e.TeamId);

            modelBuilder.Entity<User>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasIndex(e => e.Name) 
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(e => e.AccountName)
                .IsUnique();
        }
    }
}