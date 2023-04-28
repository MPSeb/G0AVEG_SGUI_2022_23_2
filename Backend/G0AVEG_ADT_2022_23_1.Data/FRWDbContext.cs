
using G0AVEG_ADT_2022_23_1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace G0AVEG_ADT_2022_23_1.Data
{
    public class FRWDbContext : DbContext
    {

        public DbSet<Wood> Woods { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        //public FRWDbContext()
        //{
        //    Database.EnsureCreated();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging().UseLazyLoadingProxies();


            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data.mdf;Integrated Security=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Furniture>(x =>
            x.HasOne(w => w.wood)
            .WithMany(r => r.Furnitures)
            .HasForeignKey(i => i.WoodUsed)
            .OnDelete(DeleteBehavior.NoAction)
            );

            modelBuilder.Entity<Furniture>(x =>
            x.HasOne(i => i.retailer)
            .WithMany(y => y.furnitures)
            .HasForeignKey(x => x.RetailerId)
            .OnDelete(DeleteBehavior.NoAction)
            );



            Retailer Fella = new Retailer { Id = 1, Name = "Fella" };
            Retailer Chef = new Retailer { Id = 2, Name = "Chef" };
            Retailer Leg = new Retailer { Id = 3, Name = "Leg" };


            var Furnitures = new List<Furniture>()
            {

                new Furniture()  {Id = 1, Name = "Cupboard", WoodUsed = 2, retailer = Chef},
                new Furniture()  {Id = 2, Name = "Kitchen Cabinet", WoodUsed = 4, retailer = Fella},
                new Furniture()  {Id = 3, Name = "Countertop", WoodUsed = 6, retailer = Leg},

                new Furniture()  {Id = 4, Name = "Drawer", WoodUsed = 5, retailer = Leg},
                new Furniture()  {Id = 5, Name = "Wardrobe", WoodUsed = 3, retailer = Chef},
                new Furniture()  {Id = 6, Name = "Bed", WoodUsed = 1, retailer = Leg},
                new Furniture()  {Id = 7, Name = "Closet", WoodUsed = 6, retailer = Chef},

                new Furniture()  {Id = 8, Name = "Coffee Table", WoodUsed = 4, retailer = Fella},
                new Furniture()  {Id = 9, Name = "Liquor Cabinet", WoodUsed = 2, retailer = Leg},
                new Furniture()  {Id = 10, Name = "Couch", WoodUsed = 4, retailer = Chef},

                new Furniture()  {Id = 11, Name = "Dining Table", WoodUsed = 5, retailer = Fella},
                new Furniture()  {Id = 12, Name = "Wine Rack", WoodUsed = 1, retailer = Fella},

                new Furniture()  {Id = 13, Name = "Table", WoodUsed = 1, retailer = Leg},
                new Furniture()  {Id = 14, Name = "Drawing board", WoodUsed = 6, retailer = Chef},

                new Furniture()  {Id = 15, Name = "Bookcase", WoodUsed = 3, retailer = Fella},
                new Furniture()  {Id = 16, Name = "Desk", WoodUsed = 2, retailer = Leg},

            };
            var Oak = new Wood() { Id = 1, Name = "Oak wood", Price = 1000 };
            var Teak = new Wood() { Id = 2, Name = "Teak wood", Price = 2000 };
            var Mahogany = new Wood() { Id = 3, Name = "Mahogany wood", Price = 3000 };
            var Maple = new Wood() { Id = 4, Name = "Maple wood", Price = 1400 };
            var Walnut = new Wood() { Id = 5, Name = "Walnut wood", Price = 2500 };
            var Pine = new Wood() { Id = 6, Name = "Pine wood", Price = 800 };





            modelBuilder.Entity<Retailer>().HasData(Fella, Chef, Leg);
            modelBuilder.Entity<Furniture>().HasData(Furnitures);
            modelBuilder.Entity<Wood>().HasData(Oak, Teak, Mahogany, Maple, Walnut, Pine);
        }
    }
}
