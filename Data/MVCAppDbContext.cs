﻿using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<ItemClient>().HasKey(ic => new
            {
                ic.ItemId,
                ic.ClientId
            });
            modelBuilder.Entity<ItemClient>().HasOne(i => i.Item)
                .WithMany(ic => ic.ItemClients).HasForeignKey(i => i.ItemId);
            modelBuilder.Entity<ItemClient>().HasOne(c => c.Client)
                .WithMany(ic => ic.ItemClients).HasForeignKey(i => i.ClientId);
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 4, Name="Pencil", Price=40,SerialNumberId=10}
                );
            modelBuilder.Entity<SerialNumber>().HasData(
                new SerialNumber { Id = 10, Name = "MMRRA", ItemId = 4 }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics"},
                new Category { Id = 2, Name = "Books"}
                );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ItemClient> ItemClients { get; set; }
    }
}
