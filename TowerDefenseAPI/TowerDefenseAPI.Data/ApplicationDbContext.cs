using Microsoft.EntityFrameworkCore;
using TowerDefenseAPI.Domain.Models;

namespace TowerDefenseAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Login = "Menn",
                    Password = "1234",
                    Role = "Player"
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Login = "Fille",
                    Password = "4321",
                    Role = "Admin"
                });


        }
    }
}
