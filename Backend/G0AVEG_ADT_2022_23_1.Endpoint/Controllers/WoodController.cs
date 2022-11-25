using G0AVEG_ADT_2022_23_1.Logic;
using G0AVEG_ADT_2022_23_1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace G0AVEG_HFT_2021221.Endpoint.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class WoodController : ControllerBase
    {
        IWoodLogic wl;

        public WoodController(IWoodLogic wl)
        {
            this.wl = wl;
        }

        // GET: /wood
        [HttpGet]
        public IEnumerable<Wood> Get()
        {
            return wl.GetWoods();
        }

        // GET /wood/1
        [HttpGet("{id}")]
        public Wood Get(int id)
        {
            return wl.GetWood(id);
        }

        // POST: /wood
        [HttpPost]
        public void Post(Wood wood)
        {
            wl.CreateWood(wood);
        }

        // PUT: /wood
        [HttpPut]
        public void Put([FromBody] Wood value)
        {
            wl.UpdateWood(value);
        }

        // DELETE: /wood/1
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            wl.RemoveWood(id);
        }
    }
}