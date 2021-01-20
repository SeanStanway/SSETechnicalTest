using Microsoft.Extensions.Logging;
using SqlLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SqlLibrary
{
    /// <summary>
    /// SQL Library to perform all read operations to MS SQL server.
    /// Code smells on the try-catch-teardown of the sql calls, 
    /// </summary>

    public class ProductsSql : IProductsSql
    {
        private SqlConnection _connection;
        private ILogger _logger;

        public ProductsSql(SqlConnection connection, ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public IEnumerable<Product> GetFeaturedProducts()
        {
            using (SqlCommand command = new SqlCommand("FeaturedProducts", _connection))
            {
                var reader = GetDataFromDatabase(command);

                return GetProductListFromSqlData(reader);
            }
        }

        public IEnumerable<Category> GetAvailableCategories()
        {
            var categoryList = new List<Category>();

            using (SqlCommand command = new SqlCommand("AvailableCategories", _connection))
            {
                var reader = GetDataFromDatabase(command);

                try
                {
                    while (reader.Read())
                    {
                        categoryList.Add(
                            new Category { Id =  (int)reader["Id"], Name = reader["Name"].ToString()}
                        );
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed reading SQL data");
                }
                finally
                {
                    reader.Close();
                    _connection.Close();
                }

                return categoryList;
            }
        }

        public IEnumerable<Product> GetProductsByCategoryName(string categoryName)
        {
            using (SqlCommand command = new SqlCommand("ProductsByCategoryName", _connection))
            {
                command.Parameters.Add("@categoryName", System.Data.SqlDbType.NVarChar);
                command.Parameters["@categoryName"].Value = categoryName;

                var reader = GetDataFromDatabase(command);

                return GetProductListFromSqlData(reader);
            }
        }

        private SqlDataReader GetDataFromDatabase(SqlCommand command)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                _connection.Open();
                return command.ExecuteReader();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read from database");
                throw;
            }
        }

        private IEnumerable<Product> GetProductListFromSqlData(SqlDataReader reader)
        {
            var productList = new List<Product>();

            try
            {
                while (reader.Read())
                {
                    productList.Add(ReturnProductFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed reading SQL data");
            }
            finally
            {
                reader.Close();
                _connection.Close();
            }

            return productList;
        }

        private Product ReturnProductFromReader(SqlDataReader reader)
        {
            return new Product()
            {
                SKU = (int)reader["SKU"],
                CategoryId = (int)reader["CategoryId"],
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Price = Convert.ToDouble(reader["Price"])
            };
        }
    }
}
