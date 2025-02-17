using Microsoft.EntityFrameworkCore;
using MVCApp.Models;

namespace MVCApp.Data
{
    public class MVCAppDbContext : DbContext
    {
        public MVCAppDbContext(DbContextOptions<MVCAppDbContext> option) : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 4, Name="Pencil", Price=40,SerialNumberId=10}
                );
            modelBuilder.Entity<SerialNumber>().HasData(
                new SerialNumber { Id = 10, Name = "MMRRA", ItemId = 4 }
                );
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }
    }
}
