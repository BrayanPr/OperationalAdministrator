using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class OperationalAdministratorContext : DbContext
    {

        public OperationalAdministratorContext (DbContextOptions<OperationalAdministratorContext> options) : base (options) 
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>()
                .HasOne(m=> m.Sender)
                .WithMany(t => t.SendedOperations)
                .HasForeignKey(t => t.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Operation>()
               .HasOne(m => m.Receiver)
               .WithMany(t => t.ReceivedOperations)
               .HasForeignKey(t => t.ReceiverId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}