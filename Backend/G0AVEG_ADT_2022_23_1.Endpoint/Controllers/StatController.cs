using G0AVEG_ADT_2022_23_1.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace G0AVEG_ADT_2022_23_1.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class StatController : ControllerBase
    {
        IFurnitureLogic fl;
        IWoodLogic wl;
        IRetailerLogic rl;

        public StatController(IFurnitureLogic fl, IWoodLogic wl, IRetailerLogic rl)
        {
            this.fl = fl;
            this.rl = rl;
            this.wl = wl;
        }

        // GET: stat/woodidsforretailer/1
        [HttpGet("{retailerId}")]
        public int[] woodIdsForRetailers(int retailerId)
        {
            return fl.woodsIdsForRetailer(retailerId);
        }

        // GET: stat/doesretailersellwood/1/1
        [HttpGet("{retailerId}/{woodId}")]
        public bool DoesRetailerSellWood(int retailerId, int woodId)
        {
            return fl.DoesRetailerSellWood(retailerId, woodId);
        }

        // GET: stat/avgwoodpriceofretailer/1
        [HttpGet("{retailerId}")]
        public int avgWoodPriceOfRetailer(int retailerId)
        {
            return fl.avgWoodPriceOfRetailer(retailerId);
        }

        // GET: stat/averagefurnperretailer/1
        [HttpGet]
        public double AverageFurnPerRetailer()
        {
            return fl.AverageFurnPerRetailer();
        }

        // GET: stat/woodusedinfurniturebelowprice/1400
        [HttpGet("{priceLim}")]
        public int WoodUsedInFurnBelowPrice(int priceLim)
        {
            return fl.WoodUsedInFurnBelowPrice(priceLim);
        }
    }
}