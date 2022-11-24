using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Logic
{
    public interface IRetailerLogic
    {
        IEnumerable<Retailer> GetRetailers();
        Retailer GetRetailer(int id);
        void CreateRetailer(Retailer entry);
        void RemoveRetailer(int id);
        void UpdateRetailer(Retailer entry);
    }
}
