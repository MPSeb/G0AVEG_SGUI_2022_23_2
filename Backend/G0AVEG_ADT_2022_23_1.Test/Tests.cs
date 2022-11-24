using G0AVEG_ADT_2022_23_1.Logic;
using G0AVEG_ADT_2022_23_1.Repository;
using Moq;
using NUnit.Framework;
using System;

namespace G0AVEG_ADT_2022_23_1.Test
{
    [TestFixture]
    public class Tests
    {
        FurnitureLogic fLogic;
        RetailerLogic rLogic;
        WoodLogic wLogic;

        [SetUp]
        public void Initialise()
        {
            var mockFurnitureRepository = new Mock<IFurnitureRepository>();
            var mockRetailerRepository = new Mock<IRetailerRepository>();
            var mockWoodRepository = new Mock<IWoodRepository>();


        }
    }
}
