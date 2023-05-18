using G0AVEG_ADT_2022_23_1.Logic;
using G0AVEG_ADT_2022_23_1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace G0AVEG_ADT_2022_23_1.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class FurnitureController : ControllerBase
    {

        IFurnitureLogic fl;
        public FurnitureController(IFurnitureLogic fl)
        {
            this.fl = fl;
        }

        // GET: /furniture
        [HttpGet]
        public IEnumerable<Furniture> Get()
        {
            return fl.GetFurnitures();
        }

        // GET /furniture/1
        [HttpGet("{id}")]
        public Furniture Get(int id)
        {
            return fl.GetFurniture(id);
        }

        // POST /furniture
        [HttpGet]
        public void Post(Furniture furniture)
        {
            fl.CreateFurniture(furniture);
        }

        // PUT /furniture
        [HttpPut]
        public void Put([FromBody] Furniture value)
        {
            fl.UpdateFurniture(value);
        }

        // DELETE /furniture/1
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            fl.RemoveFurniture(id);
        }

    }
}