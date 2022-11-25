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
    public class RetailerController : ControllerBase
    {
        IRetailerLogic rl;

        public RetailerController(IRetailerLogic rl)
        {
            this.rl = rl;
        }
        // GET: /retailer
        [HttpGet]
        public IEnumerable<Retailer> Get()
        {
            return rl.GetRetailers();
        }

        // GET /retailer/2
        [HttpGet("{id}")]
        public Retailer Get(int id)
        {
            return rl.GetRetailer(id);
        }

        // POST /retailer
        [HttpPost]
        public void Post(Retailer retailer)
        {
            rl.CreateRetailer(retailer);
        }

        // PUT /retaielr
        [HttpPut]
        public void Put([FromBody] Retailer value)
        {
            rl.UpdateRetailer(value);
        }
        // DELETE /retailer/1
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rl.RemoveRetailer(id);
        }
    }
}