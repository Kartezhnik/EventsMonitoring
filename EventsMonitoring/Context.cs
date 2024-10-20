using EventsMonitoring.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsMonitoring
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>().HasOne(u => u.Event).WithMany(e => e.Users).HasForeignKey(u => u.EventInfoKey);
            modelBuilder.Entity<User>().HasOne(u => u.Token).WithOne(r => r.User).HasForeignKey<Tokens>(r => r.UserId);
        }
        

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Tokens> Tokens { get; set; } = null!;

    }
}
