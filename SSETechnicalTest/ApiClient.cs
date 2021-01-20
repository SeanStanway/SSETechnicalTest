using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SSETechnicalTest.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SSETechnicalTest
{
    public class ApiClient : IApiClient
    {
        static HttpClient client = new HttpClient();
        private readonly ApiOptions _apiOptions;

        public ApiClient(IOptionsMonitor<ApiOptions> optionsMonitor)
        {
            _apiOptions = optionsMonitor.CurrentValue;
            client.BaseAddress = new Uri(_apiOptions.BaseUrl);
        }

        public async Task<List<Product>> GetFeaturedProducts()
        {
            HttpResponseMessage response = await client.GetAsync(
               "Api/Products/FeaturedProducts");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                WriteUnsuccessfulMessages(response);
                return null;
            }

            var productList = await response.Content.ReadAsAsync<List<Product>>().ConfigureAwait(false);

            return productList;
        }

        public async Task<List<Category>> GetAvailableCategories()
        {
            HttpResponseMessage response = await client.GetAsync(
               "Api/Products/AvailableCategories");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                WriteUnsuccessfulMessages(response);
                return null;
            }

            var categoryList = await response.Content.ReadAsAsync<List<Category>>().ConfigureAwait(false);

            return categoryList;
        }

        public async Task<List<Product>> GetProductsByCategoryName(string categoryName)
        {
            HttpResponseMessage response = await client.GetAsync(
               $"Api/Products/ProductsByCategory/{categoryName}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                WriteUnsuccessfulMessages(response);
                return null;
            }

            var productList = await response.Content.ReadAsAsync<List<Product>>().ConfigureAwait(false);

            return productList;
        }

        private void WriteUnsuccessfulMessages(HttpResponseMessage response)
        {
            Console.WriteLine("Warning: Unsuccessful call, please see below");
            Console.Write($"Response: {response.Content.ReadAsStringAsync().Result}");
        }
    }
}
