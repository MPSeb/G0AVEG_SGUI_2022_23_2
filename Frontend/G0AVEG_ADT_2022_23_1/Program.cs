using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;

namespace G0AVEG_ADT_2022_23_1.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(6000);

            RestService rest = new RestService("http://localhost:63958");

            rest.Post(new Wood()
            {
                Id = 1,
                Name = "Bamboo"
            }, "wood");
            rest.Put(new Wood()
            {
                Id = 2,
                Name = "Charcoal"
            }, "wood");

            rest.Post(new Furniture()
            {
                Id = 1,
                Name = "Spring trap",
                WoodUsed = 1,
                RetailerId = 1
            }, "furniture");
            rest.Put(new Furniture()
            {
                Id = 2,
                Name = "Glass shards",
                WoodUsed = 2,
                RetailerId = 2

            }, "furniture");

            rest.Put(new Retailer()
            {
                Id = 1,
                Name = "Rugpull corp"
            }, "retailer");
            rest.Put(new Retailer()
            {
                Id = 2,
                Name = "Scammers"
            }, "retailer");


            var furnitures = rest.Get<Furniture>("furniture");
            var woods = rest.Get<Wood>("wood");
            var rooms = rest.Get<Retailer>("retailer");


            var furniture = rest.Get<Furniture>("furniture/1");
            var wood = rest.Get<Wood>("wood/1");
            var room = rest.Get<Retailer>("retailer/1");

            rest.Delete(3, "furniture");
            rest.Delete(3, "wood");
            rest.Delete(3, "room");

            int[] woodIdsForRetailer = rest.GetSingle<int[]>("stat/woodidsforretailer/1");
            bool doesretailersellwood = rest.GetSingle<bool>("stat/doesretailersellwood/1/1");
            int avgwoodpriceofretailer = rest.GetSingle<int>("stat/avgwoodpriceofretailer/1");
            double averagefurnperretailer = rest.GetSingle<double>("stat/averagefurnperretailer/1");
            int woodusedinfurnbelowprice = rest.GetSingle<int>("stat/woodusedinfurniturebelowprice/1400");
        }
    }
}
