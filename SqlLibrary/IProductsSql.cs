using Microsoft.Extensions.Logging;
using SqlLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary
{
    public interface IProductsSql
    {
        IEnumerable<Product> GetFeaturedProducts();
        IEnumerable<Category> GetAvailableCategories();
        IEnumerable<Product> GetProductsByCategoryName(string categoryName);
    }
}
