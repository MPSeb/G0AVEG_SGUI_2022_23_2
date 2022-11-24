using G0AVEG_ADT_2022_23_1.Models;
using G0AVEG_ADT_2022_23_1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Logic
{
    public class RetailerLogic : IRetailerLogic
    {
        public readonly IRetailerRepository _retailerRepository;

        public RetailerLogic(IRetailerRepository retailerRepository)
        {
            _retailerRepository = retailerRepository;
        }

        public void CreateRetailer(Retailer entry)
        {
            _retailerRepository.Add(entry);
        }

        public Retailer GetRetailer(int id)
        {
            return _retailerRepository.GetRetailer(id);
        }

        public IEnumerable<Retailer> GetRetailers()
        {
            throw new NotImplementedException();
        }

        public void RemoveRetailer(int id)
        {
            _retailerRepository.Remove(id);
        }

        public void UpdateRetailer(Retailer entry)
        {
            _retailerRepository.Update(entry);
        }
    }
}
