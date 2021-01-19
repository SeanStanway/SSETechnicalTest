using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApi.Controllers;
using Microsoft.Extensions.Configuration;
using SqlLibrary;

namespace UnitTesting
{
    [TestClass]
    public class ProductApiTests
    {
        private Mock<ILogger<ProductsController>> _logger;
        private Mock<IProductsSql> _sql;
        private ProductsController _controller;

        [TestInitialize]
        public void Initialise()
        {
            _logger = new Mock<ILogger<ProductsController>>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetConnectionString("test")).Returns("test");
            _controller = new ProductsController(_logger.Object, mockConfiguration.Object);
        }

        [TestMethod]
        public void GetFeaturedProducts_Returns_Success()
        {
            //Arrange

            //Act
            var result = _controller.GetFeaturedProducts();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetAvailableCategories_Returns_Success()
        {
            //Arrange

            //Act
            var result = _controller.GetAvailableCategories();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetProductByCategory_Home_Returns_Success()
        {
            //Arrange

            //Act
            var result = _controller.GetProductsByCategoryName("home");

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetProductByCategory_Garden_Returns_Success()
        {
            //Arrange

            //Act
            var result = _controller.GetProductsByCategoryName("garden");

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetProductsByCategory_UsingWhiteSpace_Returns_Succcess()
        {
            //Arrange

            //Act
            var result = _controller.GetProductsByCategoryName("   ElECTRonics    ");

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetProductByCategory_Invalid_Returns_BadRequest()
        {
            //Arrange

            //Act
            var result = _controller.GetProductsByCategoryName("invalid");

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}
