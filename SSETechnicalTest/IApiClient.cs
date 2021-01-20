using SSETechnicalTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSETechnicalTest
{
    public interface IApiClient
    {
        Task<List<Category>> GetAvailableCategories();
        Task<List<Product>> GetFeaturedProducts();
        Task<List<Product>> GetProductsByCategoryName(string categoryName);
    }
}