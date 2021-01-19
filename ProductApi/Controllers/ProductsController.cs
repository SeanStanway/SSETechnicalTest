using Microsoft.AspNetCore.Mvc;
using SqlLibrary;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("Api/Products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private IProductsSql _sql;
        private readonly List<string> _acceptedCategories = new List<string>() { "home", "garden", "electronics", "fitness", "toys" };

        public ProductsController(ILogger<ProductsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _sql = new ProductsSql(new SqlConnection(configuration.GetConnectionString("mmtStore")), _logger);
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
            var result = _sql.GetAvailableCategories();

            return Ok(result);
        }

        [HttpGet]
        [Route("ProductsByCategory/{categoryName}")]
        public IActionResult GetProductsByCategoryName(string categoryName)
        {
            var formattedCategory = categoryName.Trim().ToLower();

            if (!_acceptedCategories.Contains(formattedCategory))
            {
                return BadRequest("Invalid category");
            }

            var result = _sql.GetProductsByCategoryName(formattedCategory);

            return Ok(result);
        }
    }
}
