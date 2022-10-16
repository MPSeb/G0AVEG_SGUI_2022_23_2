﻿
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace G0AVEG_ADT_2022_23_1.Data
{
    public class FRWDbContext : DbContext
    {
        public FRWDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Models.Wood> Woods { get; set; }
        public DbSet<Models.Furniture> Furnitures { get; set; }
        public DbSet<Models.Retailer> Retailers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging().UseLazyLoadingProxies();


            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CarsDB.mdf;Integrated Security=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Furniture>(x =>
            x.HasOne(w => w.wood)
            .WithMany(r =>r.Furnitures)
            .HasForeignKey(i => i.WoodUsed)
            .OnDelete(DeleteBehavior.NoAction)            
            );

            modelBuilder.Entity<Models.Furniture>(x =>
            x.HasMany(i =>i.Retailers)
            .WithMany(y => y.furnitures)
            );
                


            Models.Retailer Fella = new Models.Retailer {Id = 1, Name = "Fella"};
            Models.Retailer Chef = new Models.Retailer {Id = 2, Name = "Chef"};
            Models.Retailer Leg = new Models.Retailer {Id =3, Name = "Leg"};


            var Furnitures = new List<Models.Furniture>()
            {
                //Kitchen
                new Models.Furniture()  {Id = 1, Name = "Cupboard", WoodUsed = 2},
                new Models.Furniture()  {Id = 2, Name = "Kitchen Cabinet", WoodUsed = 4},
                new Models.Furniture()  {Id = 3, Name = "Countertop", WoodUsed = 6},
                //Bedroom
                new Models.Furniture()  {Id = 4, Name = "Drawer", WoodUsed = 5},
                new Models.Furniture()  {Id = 5, Name = "Wardrobe", WoodUsed = 3},
                new Models.Furniture()  {Id = 6, Name = "Bed", WoodUsed = 1},
                new Models.Furniture()  {Id = 7, Name = "Closet", WoodUsed = 6},
                //Living Room
                new Models.Furniture()  {Id = 8, Name = "Coffee Table", WoodUsed = 4},
                new Models.Furniture()  {Id = 9, Name = "Liquor Cabinet", WoodUsed = 2},
                new Models.Furniture()  {Id = 10, Name = "Couch", WoodUsed = 4},
                //Dining Room
                new Models.Furniture()  {Id = 11, Name = "Dining Table", WoodUsed = 5},
                new Models.Furniture()  {Id = 12, Name = "Wine Rack", WoodUsed = 1},
                //Home Office
                new Models.Furniture()  {Id = 13, Name = "Table", WoodUsed = 1},
                new Models.Furniture()  {Id = 14, Name = "Drawing board", WoodUsed = 6},
                //Library
                new Models.Furniture()  {Id = 15, Name = "Bookcase", WoodUsed = 3},
                new Models.Furniture()  {Id = 16, Name = "Desk", WoodUsed = 2},

            };
            var Oak = new Models.Wood() { Id = 1, Name = "Oak wood", Price = 1000 };
            var Teak = new Models.Wood() { Id = 2, Name = "Teak wood", Price = 2000 };
            var Mahogany = new Models.Wood() { Id = 3, Name = "Mahogany wood", Price = 3000 };
            var Maple = new Models.Wood() { Id = 4, Name = "Maple wood", Price = 1400 };
            var Walnut = new Models.Wood() { Id = 5, Name = "Walnut wood", Price = 2500 };
            var Pine = new Models.Wood() { Id = 6, Name = "Pine wood", Price = 800 };





            modelBuilder.Entity<Models.Retailer>().HasData(Fella, Chef, Leg);
            modelBuilder.Entity<Models.Furniture>().HasData(Furnitures);
            modelBuilder.Entity<Models.Wood>().HasData(Oak, Teak,Mahogany,Maple,Walnut,Pine);
        }
    }
}