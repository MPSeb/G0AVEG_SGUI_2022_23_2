using G0AVEG_ADT_2022_23_1.Models;
using G0AVEG_ADT_2022_23_1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Logic
{
    public class FurnitureLogic : IFurnitureLogic
    {
        public readonly IFurnitureRepository _furnitureRepository;
        public readonly IWoodRepository _woodRepository;
        public readonly IRetailerRepository _retailerRepository;

        public FurnitureLogic(IFurnitureRepository furnitureRepository, IWoodRepository woodRepository, IRetailerRepository retailerRepository)
        {
            _furnitureRepository = furnitureRepository;
            _woodRepository = woodRepository;
            _retailerRepository = retailerRepository;
        }

        public void AddFurnitureToWood(int furnitureId, int woodId)
        {
            Furniture furniture = _furnitureRepository.GetFurniture(furnitureId);

            if (furniture == null)
            {
                throw new Exception("Furniture doesn't exist");
            }

            Wood wood = _woodRepository.GetWood(woodId);

            if (wood == null)
            {
                throw new Exception("Wood is not real");
            }

            furniture.WoodUsed = wood.Id;

            _furnitureRepository.Update(furniture);
        }

        public void RemoveFurnitureFromWood(int furnitureId, int woodId)
        {
            Furniture furniture = _furnitureRepository.GetFurniture(furnitureId);

            if(furniture== null)
            {
                throw new Exception("Furniture does not exist");
            }

            Wood wood = _woodRepository.GetWood(woodId);

            if(wood == null)
            {
                throw new Exception("Wood does not exist");
            }

            furniture.wood = null;

            _furnitureRepository.Update(furniture);
        }
        public void AddRetailerToWood(int furnitureId, int retailId)
        {
            Furniture furniture = _furnitureRepository.GetFurniture(furnitureId);

            if (furniture == null)
            {
                throw new Exception("Furniture doesn't exist");
            }

            Retailer retailer = _retailerRepository.GetRetailer(retailId);

            if (retailer == null)
            {
                throw new Exception("Retailer is not real");
            }

            furniture.RetailerId = retailer.Id;

            _furnitureRepository.Update(furniture);
        }

        public void RemoveFurnitureFromRetailer(int furnitureId, int retailId)
        {
            Furniture furniture = _furnitureRepository.GetFurniture(furnitureId);

            if (furniture == null)
            {
                throw new Exception("Furniture does not exist");
            }

            Retailer retailer = _retailerRepository.GetRetailer(retailId);

            if (retailer == null)
            {
                throw new Exception("Retailer does not exist");
            }

            furniture.retailer = null;

            _furnitureRepository.Update(furniture);
        }

        public void CreateFurniture(Furniture entry)
        {
            if(entry.Name == null)
            {
                throw new Exception("Name cannot be empty");
            }
            _furnitureRepository.Add(entry);
        }

        public Furniture GetFurniture(int id)
        {
            return _furnitureRepository.GetFurniture(id);
        }

        public IEnumerable<Furniture> GetFurnitures()
        {
            return _furnitureRepository.GetAll();
        }

        public void RemoveFurniture(int id)
        {
            _furnitureRepository.Remove(id);
        }

        public void UpdateFurniture(Furniture entry)
        {
            _furnitureRepository.Update(entry);
        }

        //Non-CRUD methods

        public int[] woodsIdsForRetailer(int retailerId)
        {
            IEnumerable<Furniture> furnitures = _furnitureRepository.GetAll().ToList().Where(f => f.RetailerId == retailerId);
            IEnumerable<Wood> woods = _woodRepository.GetAll().ToList();
            int add = 0;
            for (int i = 1; i < woods.Count(); i++)
            {
                if (furnitures.Where(f => f.WoodUsed == i).Count() != 0)
                {
                    add++;
                }
            }
            int[] array = new int[add];
            int counter = 0;
            for (int i = 1; i < woods.Count(); i++)
            {
                if (furnitures.Where(f => f.WoodUsed == i).Count() != 0)
                {
                    array[counter] = i;
                    counter++;
                }
            }
            return array;
        }

        public bool DoesRetailerSellWood(int retailerId, int woodId)
        {
            Retailer retailer = _retailerRepository.GetRetailer(retailerId);
            bool result = false;
            foreach (var f in retailer.furnitures)
            {
                if(f.WoodUsed == woodId)
                {
                    result = true;
                }
            }
            return result;
        }

        public int avgWoodPriceOfRetailer(int retailerId)
        {
            Retailer retailer = _retailerRepository.GetRetailer(retailerId);

            int count = 0;
            int price = 0;
            foreach (var f in retailer.furnitures)
            {
                count++;
                if (f.WoodUsed != null)
                {
                    int woodId = f.WoodUsed.Value;
                    Wood w = _woodRepository.GetWood(woodId);
                    price += w.Price;
                }
            }
            return price / count;
        }

        public double AverageFurnPerRetailer()
        {
            int furnNums = _furnitureRepository.GetAll().Count();
            int retailerNums = _retailerRepository.GetAll().Count();
            double value = furnNums / retailerNums;
            return value;
        }


        public int WoodUsedInFurnBelowPrice(int priceLim)
        {
            IEnumerable<Wood> woods = _woodRepository.GetAll().ToList().Where(t => t.Price <= priceLim);
            int number = _woodRepository.GetAll().ToList().Count() + 1;
            IEnumerable<Furniture> furnitures = _furnitureRepository.GetAll().ToList();
            int[] array = new int[number];
            int counter = 0;
            for (int i = 0; i <= number; i++)
            {
                if (woods.Where(w => w.Id == i).Count() != 0)
                {
                    array[counter] = i;
                    counter++;
                }
            }
            int result = 0;
            for (int i = 0; i <= counter; i++)
            {
                result = result + furnitures.Where(f => f.WoodUsed == array[i]).Count();
            }
            return result;

        }

    }
}
