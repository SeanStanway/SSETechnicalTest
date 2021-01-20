using System.Threading.Tasks;

namespace SSETechnicalTest
{
    public interface IExecutionPaths
    {
        Task ExecuteAvailableCategoriesRequest();
        Task ExecuteFeaturedProductsRequest();
        Task ExecuteProductsFromCategoryNameRequest(string name);
    }
}