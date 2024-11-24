using Domain.Entities;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain
{
    public class Context : DbContext
    {
        private readonly IConfiguration configuration;

        public Context(DbContextOptions<Context> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;  
        }
        public Context() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(u => u.Event).WithMany(e => e.Users).HasForeignKey(u => u.EventInfoKey).OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
