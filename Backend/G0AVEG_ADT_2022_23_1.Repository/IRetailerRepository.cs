using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Repository
{
    public interface IRetailerRepository
    {
        Retailer Read(int id);
        Retailer GetRetailer(int id);
        void Add(Retailer entry);
        void Remove(int id);
        void Update(Retailer entry);
    }
}
