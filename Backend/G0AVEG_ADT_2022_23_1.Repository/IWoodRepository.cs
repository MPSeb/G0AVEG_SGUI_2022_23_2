using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Repository
{
    public interface IWoodRepository
    {
        Wood Read(int id);
        Wood GetWood(int id);
        void Add(Wood entry);
        void Remove(int id);
        void Update(Wood entry);

        IQueryable<Wood> GetAll();

    }
}
