﻿using Moq;
using NUnit.Framework;
using Store.Domain.Contracts;
using Store.Domain.Models;
using Store.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.CartControllerTests
{
    [TestFixture]
    class CartControllerTest
    {
        [Test]
        public void TestCartController_ShouldAddToCartCorrectly()
        {
            var mockedRepository = new Mock<IProductRepository>();

            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(p => p.ProductID).Returns(1);
            mockedFirstProduct.SetupGet(p => p.Name).Returns("P1");

            mockedRepository.SetupGet(r => r.Products).Returns(new List<IProduct> { mockedFirstProduct.Object });

            var cart = new Cart();

            var controller = new CartController(mockedRepository.Object);

            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Product, mockedFirstProduct.Object);
        }

        [Test]
        public void TestCartController_WhenAddToCart_ShouldGoToCartScreen()
        {
            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(p => p.ProductID).Returns(1);
            mockedFirstProduct.SetupGet(p => p.Name).Returns("P1");

            var mockedRepository = new Mock<IProductRepository>();
            mockedRepository.SetupGet(r => r.Products).Returns(new List<IProduct> { mockedFirstProduct.Object });

            var cart = new Cart();

            var controller = new CartController(mockedRepository.Object);

            var result = controller.AddToCart(cart, 1, "myUrl");

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }
    }
}
