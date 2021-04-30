using System.Collections.Immutable;
using System.Data.Common;
using LawnKeeper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace LawnKeeper.DAL
{
    public sealed class LawnKeeperDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public LawnKeeperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Lawn> Lawns { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Lawns)
                .WithOne(l => l.Owner);
        }
    }
}