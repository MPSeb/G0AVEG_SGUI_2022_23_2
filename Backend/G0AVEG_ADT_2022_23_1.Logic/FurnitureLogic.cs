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
            throw new NotImplementedException();
        }

        public void RemoveFurniture(int id)
        {
            _furnitureRepository.Remove(id);
        }

        public void UpdateFurniture(Furniture entry)
        {
            _furnitureRepository.Update(entry);
        }








    }
}
