using SSETechnicalTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SSETechnicalTest
{
    public class ExecutionPaths : IExecutionPaths
    {
        private IApiClient _apiClient;

        public ExecutionPaths(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task ExecuteFeaturedProductsRequest()
        {
            Console.WriteLine("Processing FeaturedProducts Request");

            var apiReturn = await _apiClient.GetFeaturedProducts().ConfigureAwait(false);

            if (apiReturn != null)
            {
                Console.WriteLine("Product list retrieved, writing to text file");

                var filepath = $"featuredProducts-{DateTime.Now.ToLongTimeString().Replace(":", "")}.txt";

                File.WriteAllText(filepath, JsonSerializer.Serialize(apiReturn));

                Console.WriteLine($"Product list written to: {filepath}");
            }
        }

        public async Task ExecuteAvailableCategoriesRequest()
        {
            Console.WriteLine("Processing AvailableCategories Request");

            var apiReturn = await _apiClient.GetAvailableCategories().ConfigureAwait(false);

            if (apiReturn != null)
            {
                //Strip IDs off results
                var nameList = new List<String>();

                foreach (Category category in apiReturn)
                {
                    nameList.Add(category.Name);
                }

                Console.WriteLine("Category name list retrieved, writing to text file");

                var filepath = $"availableCategories-{DateTime.Now.ToLongTimeString().Replace(":", "")}.txt";

                File.WriteAllLines(filepath, nameList);

                Console.WriteLine($"Category name list written to: {filepath}");
            }
        }

        public async Task ExecuteProductsFromCategoryNameRequest(string name)
        {
            Console.WriteLine("Processing ProductsByCategoryName Request");

            var apiReturn = await _apiClient.GetProductsByCategoryName(name).ConfigureAwait(false);

            if (apiReturn != null)
            {
                Console.WriteLine("Product list retrieved, writing to text file");

                var filepath = $"productsByCategoryName-{DateTime.Now.ToLongTimeString().Replace(":", "")}.txt";

                File.WriteAllText(filepath, JsonSerializer.Serialize(apiReturn));

                Console.WriteLine($"Product list written to: {filepath}");
            }
        }
    }
}
