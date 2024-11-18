using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain
{
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;

        public Context(DbContextOptions<Context> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(u => u.Event).WithMany(e => e.Users).HasForeignKey(u => u.EventInfoKey);
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}

