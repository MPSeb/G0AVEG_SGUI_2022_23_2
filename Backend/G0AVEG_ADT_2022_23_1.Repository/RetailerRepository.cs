using G0AVEG_ADT_2022_23_1.Data;
using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Repository
{
    public class RetailerRepository : IRetailerRepository
    {
        FRWDbContext dbContext;

        public RetailerRepository(FRWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Retailer entity)
        {
            dbContext.Retailers.Add(entity);
            dbContext.SaveChanges();
        }

        public void Remove(int Id)
        {
            var RetailerToRemove = GetRetailer(Id);

            dbContext.Retailers.Remove(RetailerToRemove);
            dbContext.SaveChanges();
        }

        public Retailer Read(int Id)
        {
            return dbContext.Retailers.FirstOrDefault(r => r.Id == Id);
        }

        public Retailer GetRetailer(int Id)
        {
            return dbContext.Retailers.SingleOrDefault(r => r.Id == Id);
        }

        public void Update(Retailer entity)
        {

            var RetailerToUpdate = GetRetailer(entity.Id);
            RetailerToUpdate.Name = entity.Name;
            RetailerToUpdate.furnitures = entity.furnitures;
            dbContext.SaveChanges();
        }
    }
}
