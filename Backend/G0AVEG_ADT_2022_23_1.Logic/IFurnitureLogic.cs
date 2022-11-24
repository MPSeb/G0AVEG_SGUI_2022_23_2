using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Logic
{
    public interface IFurnitureLogic
    {
        IEnumerable<Furniture> GetFurnitures();
        Furniture GetFurniture(int id);
        void CreateFurniture(Furniture entry);
        void RemoveFurniture(int id);
        void UpdateFurniture(Furniture entry);
    }
}
