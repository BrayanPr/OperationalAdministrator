using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DB
{
    public class OperationalAdministratorContext : DbContext
    {
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