using G0AVEG_ADT_2022_23_1.Data;
using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Repository
{
    public class WoodRepository : IWoodRepository
    {
        FRWDbContext dbContext;

        public WoodRepository(FRWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Wood entity)
        {
            dbContext.Woods.Add(entity);
            dbContext.SaveChanges();
        }

        public void Remove(int Id)
        {
            var WoodToRemove = GetWood(Id);

            dbContext.Woods.Remove(WoodToRemove);
            dbContext.SaveChanges();
        }

        public Wood Read(int Id)
        {
            return dbContext.Woods.FirstOrDefault(w => w.Id == Id);
        }
        public Wood GetWood(int Id)
        {
            return dbContext.Woods.SingleOrDefault(w => w.Id == Id);
        }

        public void Update(Wood entity)
        {

            var WoodToUpdate = GetWood(entity.Id);
            WoodToUpdate.Name = entity.Name;
            WoodToUpdate.Price = entity.Price;
            WoodToUpdate.Furnitures = entity.Furnitures;
            dbContext.SaveChanges();
        }


    }
}
