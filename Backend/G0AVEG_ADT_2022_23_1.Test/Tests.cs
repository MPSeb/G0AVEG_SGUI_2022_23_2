﻿using G0AVEG_ADT_2022_23_1.Logic;
using G0AVEG_ADT_2022_23_1.Models;
using G0AVEG_ADT_2022_23_1.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var Furnitures = new List<Furniture>()
            {
                new Furniture()
                {
                    Id = 1,
                    Name = "Gaming Bamboo Chair",
                    RetailerId = 1,
                    WoodUsed = 1

                },
                new Furniture()
                {
                    Id = 2,
                    Name = "Gaming Bamboo Shelf",
                    RetailerId = 2,
                    WoodUsed = 2
                }

            }.AsQueryable();
            var Woods = new List<Wood>()
            {
                new Wood()
                {
                    Id = 1,
                    Name = "Bamboo",
                    Price = 1500
                },
                new Wood()
                {
                    Id = 2,
                    Name = "Yew",
                    Price = 500
                }
            }.AsQueryable();
            var Retailers = new List<Retailer>()
            {
                new Retailer()
                {
                    Id = 1,
                    Name = "Gamers Union",
                    furnitures = Furnitures.ToList()
                },
                new Retailer()
                {
                    Id = 2,
                    Name = "Fanatics of Furinitures",
                    furnitures = Furnitures.ToList()
                }
            }.AsQueryable();

            mockFurnitureRepository.Setup((t) => t.GetAll())
                .Returns(Furnitures);
            mockRetailerRepository.Setup((x) => x.GetAll())
                .Returns(Retailers);
            mockWoodRepository.Setup((y) => y.GetAll())
                .Returns(Woods);

            mockRetailerRepository.Setup((x) => x.GetRetailer(It.IsAny<int>())).Returns<int>((v) => Retailers.Where(r => r.Id == v).FirstOrDefault());
            mockFurnitureRepository.Setup((x) => x.GetFurniture(It.IsAny<int>())).Returns<int>((v) => Furnitures.Where(r => r.Id == v).FirstOrDefault());
            mockWoodRepository.Setup((x) => x.GetWood(It.IsAny<int>())).Returns<int>((v) => Woods.Where(r => r.Id == v).FirstOrDefault());

            rLogic = new RetailerLogic(mockRetailerRepository.Object);
            wLogic = new WoodLogic(mockWoodRepository.Object);
            fLogic = new FurnitureLogic(mockFurnitureRepository.Object, mockWoodRepository.Object, mockRetailerRepository.Object);
        }
        [Test]
        public void CreateExceptionTest()
        {
            Furniture furniture = new Furniture { Name = null, RetailerId = 1, WoodUsed = 1 };
            Assert.That(() => this.fLogic.CreateFurniture(furniture), Throws.TypeOf<Exception>());
        }
        [Test]
        public void GetAllWoodTest()
        {
            List<Wood> woods = wLogic.GetWoods().ToList();

            Assert.NotNull(woods);
            Assert.AreEqual(2, woods.Count);
        }
        [Test]
        public void GetRetailersTest()
        {
            List<Retailer> retailers = rLogic.GetRetailers().ToList();
            Assert.NotNull(retailers);
            Assert.AreEqual(2, retailers.Count);
        }
        [Test]
        public void NoExceptionTest()
        {
            Furniture furniture = new Furniture { Name = "OSB", RetailerId = 1, WoodUsed = 1 };
            Assert.That(() => this.fLogic.CreateFurniture(furniture), Throws.Nothing);
        }
        [Test]
        public void DoesRetailerSellWoodTest()
        {
            bool result = fLogic.DoesRetailerSellWood(1, 1);
            bool expected = true;
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void DoesRetailerSellWoodTest2()
        {
            bool result = fLogic.DoesRetailerSellWood(1, 3);
            bool expected = false;
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void AverageWoodPriceOfRetailerTest()
        {
            int result = fLogic.avgWoodPriceOfRetailer(1);
            int expected = 1000;
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void AvgFurnPerRetailerTest()
        {
            double value = fLogic.AverageFurnPerRetailer();
            double expected = 1;
            Assert.That(value, Is.EqualTo(expected));
        }
        [Test]
        public void WoodBelowPriceTest()
        {
            int value = fLogic.WoodUsedInFurnBelowPrice(1400);
            int expected = 1;
            Assert.That(value, Is.EqualTo(expected));
        }
        [Test]
        public void WoodBelowPriceTest2()
        {
            int value = fLogic.WoodUsedInFurnBelowPrice(1501);
            int expected = 2;
            Assert.That(value, Is.EqualTo(expected));
        }
    }
}
