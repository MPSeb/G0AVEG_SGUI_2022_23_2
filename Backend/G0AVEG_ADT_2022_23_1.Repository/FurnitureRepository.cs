using G0AVEG_ADT_2022_23_1.Data;
using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Repository
{
    public class FurnitureRepository : IFurnitureRepository
    {
        FRWDbContext dbContext;
        public FurnitureRepository(FRWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void Add(Furniture entity)
        {
            dbContext.Furnitures.Add(entity);
            dbContext.SaveChanges();
        }

        public void Remove(int Id)
        {
            var FurnitureToDelete = GetFurniture(Id);

            dbContext.Furnitures.Remove(FurnitureToDelete);
            dbContext.SaveChanges();
        }

        public Furniture Read(int Id)
        {
            return dbContext.Furnitures.FirstOrDefault(f => f.Id == Id);
        }

        public Furniture GetFurniture(int Id)
        {
            return dbContext.Furnitures.SingleOrDefault(f => f.Id == Id);
        }

        public void Update(Furniture entity)
        {

            var FurnitureToUpdate = GetFurniture(entity.Id);
            FurnitureToUpdate.Name = entity.Name;
            FurnitureToUpdate.Retailers = entity.Retailers;
            FurnitureToUpdate.WoodUsed = entity.WoodUsed;
            dbContext.SaveChanges();
        }

    }
}
