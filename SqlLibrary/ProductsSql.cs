using Microsoft.Extensions.Logging;
using SqlLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SqlLibrary
{
    public class ProductsSql
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
            var productList = new List<Product>();
            SqlDataReader reader = null;

            using (SqlCommand command = new SqlCommand("FeaturedProducts", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                _connection.Open();
                try
                {
                    reader = command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to read from database");
                }                

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
        }

        private Product ReturnProductFromReader(SqlDataReader reader)
        {
            return new Product()
            {
                SKU = reader["SKU"].ToString(),
                CategoryId = (int)reader["CategoryId"],
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Price = Convert.ToDouble(reader["Price"])
            };
        }
    }
}
