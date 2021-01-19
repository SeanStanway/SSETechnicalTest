using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApi.Controllers;
using Microsoft.Extensions.Configuration;


namespace UnitTesting
{
    [TestClass]
    public class ProductApiTests
    {
        private Mock<ILogger<ProductsController>> _logger;
        private ProductsController _controller;

        [TestInitialize]
        public void Initialise()
        {
            _logger = new Mock<ILogger<ProductsController>>();
            var configuration = new Mock<IConfiguration>();
            _controller = new ProductsController(_logger.Object, configuration.Object);
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
            var result = _controller.GetProductsByCategory("home");

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetProductByCategory_Garden_Returns_Success()
        {
            //Arrange

            //Act
            var result = _controller.GetProductsByCategory("garden");

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetProductByCategory_Invalid_Returns_BadRequest()
        {
            //Arrange

            //Act
            var result = _controller.GetProductsByCategory("invalid");

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}
