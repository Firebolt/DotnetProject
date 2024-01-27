using Microsoft.EntityFrameworkCore;

namespace FinalProject.Models
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
            public DbSet<User> Users { get; set; }
            public DbSet<Flight> Flights { get; set; }
            public DbSet<Ticket> Tickets { get; set; }
            public DbSet<Seat> Seats{ get; set; }
            public DbSet<Query> Queries { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Ticket>().HasKey(x => new {x.UID, x.FID});
            }
        }
    }

