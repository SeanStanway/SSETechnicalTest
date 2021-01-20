using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSETechnicalTest;
using SSETechnicalTest.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ExecutionPathsTests
    {
        private Mock<IConfiguration> _configuration;
        private Mock<IApiClient> _apiClient;
        private IExecutionPaths _executePaths;

        [TestInitialize]
        public void Initialise()
        {
            _configuration = new Mock<IConfiguration>();
            _apiClient = new Mock<IApiClient>();
            _executePaths = new ExecutionPaths(_apiClient.Object);
        }

        [TestCleanup]
        public void CleanUp()
        {
            string[] featuredFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "featuredProducts-*", SearchOption.TopDirectoryOnly);
            string[] availableFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "availableCategories-*", SearchOption.TopDirectoryOnly);
            string[] productsByCatFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "productsByCategoryName-*", SearchOption.TopDirectoryOnly);
        
            foreach(string file in featuredFiles)
            {
                File.Delete(file);
            }

            foreach(string file in availableFiles)
            {
                File.Delete(file);
            }

            foreach(string file in productsByCatFiles)
            {
                File.Delete(file);
            }
        }

        private IEnumerable<Product> testProducts = new List<Product>()
            {
                new Product()
                {
                    SKU = 10000,
                    CategoryId = 1,
                    Name = "hometest",
                    Description = null,
                    Price = 10.99
                },
                new Product()
                {
                    SKU = 20000,
                    CategoryId = 2,
                    Name = "gardentest",
                    Description = null,
                    Price = 132.99
                },
                new Product()
                {
                    SKU = 30000,
                    CategoryId = 3,
                    Name = "electronicstest",
                    Description = null,
                    Price = 54.99
                },
                new Product()
                {
                    SKU = 40000,
                    CategoryId = 4,
                    Name = "fitnesstest",
                    Description = null,
                    Price = 10.10
                },
                new Product()
                {
                    SKU = 50000,
                    CategoryId = 5,
                    Name = "toystest",
                    Description = null,
                    Price = 658.12
                }
            };

        private IEnumerable<Category> testCategories =  new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Home"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Garden"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Electronics"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Ftiness"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Toys"
                },
            };

        private IEnumerable<int> testFeaturedCategories = new List<int>() { 1, 2, 3 };

        [TestMethod]
        public async Task ExecuteFeaturedProducts_WillCall_GetFeaturedProducts_Once()
        {
            //Arrange
            List<Product> featuredProducts = testProducts.Where(p => testFeaturedCategories.Contains(p.CategoryId)).ToList();
            _apiClient.Setup(a => a.GetFeaturedProducts()).Returns(Task.FromResult(featuredProducts));

            //Act
            await _executePaths.ExecuteFeaturedProductsRequest();

            //Assert
            _apiClient.Verify(a => a.GetFeaturedProducts(), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteFeaturedProducts_Will_CreateFileWithFeaturedProductDummyData()
        {
            //Arrange
            List<Product> featuredProducts = testProducts.Where(p => testFeaturedCategories.Contains(p.CategoryId)).ToList();
            _apiClient.Setup(a => a.GetFeaturedProducts()).Returns(Task.FromResult(featuredProducts));

            //Act
            await _executePaths.ExecuteFeaturedProductsRequest();

            //Assert
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "featuredProducts-*", SearchOption.TopDirectoryOnly);
            var fileContent = File.ReadAllText(files.FirstOrDefault());
            Assert.IsTrue(files.Length == 1);
            Assert.IsTrue(fileContent.Contains("hometest"));
            Assert.IsFalse(fileContent.Contains("toystest"));
        }

        [TestMethod]
        public async Task ExecuteFeaturedProducts_WithNullApiReturn_WillNotWriteDataToFile()
        {
            //Arrange
            _apiClient.Setup(a => a.GetFeaturedProducts()).Returns(Task.FromResult<List<Product>>(null));

            //Act
            await _executePaths.ExecuteFeaturedProductsRequest();

            //Assert
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "featuredProducts-*", SearchOption.TopDirectoryOnly);
            Assert.IsTrue(files.Length == 0);
        }

        [TestMethod]
        public async Task ExecuteAvailableCategories_WillCall_GetAvailableCategories_Once()
        {
            //Arrange
            _apiClient.Setup(a => a.GetAvailableCategories()).Returns(Task.FromResult(testCategories.ToList()));

            //Act
            await _executePaths.ExecuteAvailableCategoriesRequest();

            //Assert
            _apiClient.Verify(a => a.GetAvailableCategories(), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteAvailableCategories_Will_CreateFileWithOnlyCategoryNamesInDummyData()
        {
            //Arrange
            _apiClient.Setup(a => a.GetAvailableCategories()).Returns(Task.FromResult(testCategories.ToList()));

            //Act
            await _executePaths.ExecuteAvailableCategoriesRequest();

            //Assert
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "availableCategories-*", SearchOption.TopDirectoryOnly);
            var fileContent = File.ReadAllText(files.FirstOrDefault());
            Assert.IsTrue(files.Length == 1);
            Assert.IsTrue(fileContent.Contains("Garden"));
            Assert.IsFalse(fileContent.Contains("2"));
        }

        [TestMethod]
        public async Task ExecuteAvailableCategories_WithNullApiReturn_WillNotWriteDataToFile()
        {
            //Arrange
            _apiClient.Setup(a => a.GetAvailableCategories()).Returns(Task.FromResult<List<Category>>(null));

            //Act
            await _executePaths.ExecuteAvailableCategoriesRequest();

            //Assert
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "availableCategories-*", SearchOption.TopDirectoryOnly);
            Assert.IsTrue(files.Length == 0);
        }

        [TestMethod]
        public async Task ExecuteProdcutsFromCategoryName_WillCall_GetProductsByCategoryName_Once()
        {
            //Arrange
            _apiClient.Setup(a => a.GetProductsByCategoryName(It.IsAny<string>())).Returns(Task.FromResult(testProducts.ToList()));

            //Act
            await _executePaths.ExecuteProductsFromCategoryNameRequest("test");

            //Assert
            _apiClient.Verify(a => a.GetProductsByCategoryName(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteProdcutsFromCategoryName_Will_CreateDataOnlyRelatedToTestName()
        {
            //Arrange
            var testCategory = testCategories.FirstOrDefault();
            var falseTestCategory = testCategories.LastOrDefault();

            Category testId = testCategories.Where(c => c.Name == testCategory.Name).FirstOrDefault();
            List<Product> productsByCategory = testProducts.Where(p => p.CategoryId == testId.Id).ToList();
            _apiClient.Setup(a => a.GetProductsByCategoryName(testCategory.Name)).Returns(Task.FromResult(productsByCategory.ToList()));

            //Act
            await _executePaths.ExecuteProductsFromCategoryNameRequest(testCategory.Name);

            //Assert
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "productsByCategoryName-*", SearchOption.TopDirectoryOnly);
            var fileContent = File.ReadAllText(files.FirstOrDefault());

            Assert.IsTrue(files.Length == 1);
            Assert.IsTrue(fileContent.Contains(testCategory.Id.ToString()));
            Assert.IsFalse(fileContent.Contains(falseTestCategory.Id.ToString()));
        }

        [TestMethod]
        public async Task ExecuteProdcutsFromCategoryName_WithInvalidCategoryName_WillNotWriteDataToFile()
        {
            //Arrange
            _apiClient.Setup(a => a.GetProductsByCategoryName(It.IsAny<string>())).Returns(Task.FromResult<List<Product>>(null));

            //Act
            await _executePaths.ExecuteProductsFromCategoryNameRequest("abcedfghijkl");

            //Assert
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "productsByCategoryName-*", SearchOption.TopDirectoryOnly);
            Assert.IsTrue(files.Length == 0);
        }
    }
}
