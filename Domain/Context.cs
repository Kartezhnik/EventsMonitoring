using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;  // Добавьте это пространство имен

namespace Domain
{
    public class Context : DbContext
    {
        private readonly IConfiguration configuration;

        // Конструктор, принимающий IConfiguration
        public Context(DbContextOptions<Context> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;  // Инициализация поля
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
                Console.WriteLine($"Connection String: {connectionString}");  // Теперь работает Console
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
