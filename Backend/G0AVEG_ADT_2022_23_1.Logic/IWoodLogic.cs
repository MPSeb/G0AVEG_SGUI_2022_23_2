using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Logic
{
    public interface IWoodLogic
    {
        IEnumerable<Wood> GetWoods();
        Wood GetWood(int id);
        void CreateWood(Wood entry);
        void RemoveWood(int id);
        void UpdateWood(Wood Entry);
    }
}
