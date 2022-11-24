using G0AVEG_ADT_2022_23_1.Models;
using G0AVEG_ADT_2022_23_1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Logic
{
    public class WoodLogic : IWoodLogic
    {
        public readonly IWoodRepository _woodRepository;

        public WoodLogic(IWoodRepository woodRepository)
        {
            _woodRepository = woodRepository;
        }
        public void CreateWood(Wood entry)
        {
            if(entry == null)
            {
                throw new Exception("Wood doesn't exist");
            }
            _woodRepository.Add(entry);
        }

        public Wood GetWood(int id)
        {
            return _woodRepository.GetWood(id);
        }

        public IEnumerable<Wood> GetWoods()
        {
            return _woodRepository.GetAll();
        }

        public void RemoveWood(int id)
        {
            _woodRepository.Remove(id);
        }

        public void UpdateWood(Wood Entry)
        {
            _woodRepository.Update(Entry);
        }
    }
}
