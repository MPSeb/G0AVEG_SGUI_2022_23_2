using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Repository
{
    public interface IFurnitureRepository
    {
        Furniture Read(int id);
        Furniture GetFurniture(int id);
        void Add(Furniture entry);
        void Update(Furniture entry);
        void Remove(int id);

        IQueryable<Furniture> GetAll();
    }
}
