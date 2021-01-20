using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using SSETechnicalTest.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SSETechnicalTest
{
    class Program
    {
        private static IApiClient _api;
        private static IExecutionPaths _executionPaths;
        private static IConfiguration _configuration;

        public static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true);

            _configuration = builder.Build();

            //Setup DI and logging instances, push appsettings for DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(cfg => cfg.AddConsole())
                .AddSingleton<IApiClient, ApiClient>()
                .AddSingleton<IExecutionPaths, ExecutionPaths>()
                .Configure<ApiOptions>(_configuration.GetSection("ApiOptions"))
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            _api = serviceProvider.GetService<IApiClient>();
            _executionPaths = serviceProvider.GetService<IExecutionPaths>();

            //Readout for user interface
            Console.WriteLine("Please enter argument based on data to search for:");
            Console.WriteLine("1 - FeaturedProducts - Gets a list of all current featured products");
            Console.WriteLine("2 - AvailableCategories - Gets a list of all available categories, listed by name");
            Console.WriteLine("3 - ProductsByCategory - Enter a category name to get a list of all related products");
            Console.WriteLine("Note: Any output will be written to a text file within the folder this application is running");

            var input = Console.ReadLine();
            var convertedInput = Convert.ToInt32(input);

            //case to determine path to take
            switch(convertedInput)
            {
                case 1:
                    await _executionPaths.ExecuteFeaturedProductsRequest();
                    break;
                case 2:
                    await _executionPaths.ExecuteAvailableCategoriesRequest();
                    break;
                case 3:
                    Console.WriteLine("Please enter the desired category to find products under");
                    var categoryName = Console.ReadLine();
                    await _executionPaths.ExecuteProductsFromCategoryNameRequest(categoryName);
                    break;
                default:
                    Console.WriteLine("Please enter a valid argument, please see numeric entries above.");
                    break;
            }
        }

    }
}
