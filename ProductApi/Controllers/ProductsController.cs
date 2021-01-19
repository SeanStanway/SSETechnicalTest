using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProductApi.Models;
using SqlLibrary;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("Api/Products")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> initialProducts = new List<Product>()
        {
            new Product()
            {
                SKU = 1,
                Name = "test",
                Description = "new test",
                Price = 9.99
            }
        };

        private static readonly List<Category> initialCategories = new List<Category>()
        {
            new Category()
            {
                Name = "category1"
            }
        };

        private static readonly List<Product> initialProductByCategory = new List<Product>()
        {
            new Product()
            {
                SKU = 2,
                Name = "test2",
                Description = "other test",
                Price = 5.99
            }
        };

        private readonly ILogger<ProductsController> _logger;
        private ProductsSql _sql;

        public ProductsController(ILogger<ProductsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            var sqlConnection = new SqlConnection(configuration.GetConnectionString("mmtStore"));
            _sql = new ProductsSql(sqlConnection, _logger);
        }

        [HttpGet]
        [Route("FeaturedProducts")]
        public IActionResult GetFeaturedProducts()
        {
            var result = _sql.GetFeaturedProducts();

            return Ok(result);
        }

        [HttpGet]
        [Route("AvailableCategories")]
        public IActionResult GetAvailableCategories()
        {
            return Ok(initialCategories);
        }

        [HttpGet]
        [Route("ProductsByCategory/{category}")]
        public IActionResult GetProductsByCategory(string category)
        {
            switch (category.ToLower())
            {
                case "home":
                    return Ok(initialProducts);
                case "garden":
                    return Ok(initialProductByCategory);
                default:
                    return BadRequest("Invalid category");
            }
        }
    }
}
