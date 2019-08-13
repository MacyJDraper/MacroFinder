using System;
using System.Collections.Generic;
using MacroFinder1.Models;
using MySql.Data.MySqlClient;
namespace MacroFinder1
{
    public class ProductRepository
    {
        private static string connectionString = System.IO.File.ReadAllText("ConnectionString.txt");
        public Product GetProduct(int id)
        {

        }
        public List<Product> GetAllProducts(int id = -1)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Product_ID, Name, Calories, Fat, Carbohydrate, Protien FROM product;";

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Product> allProducts = new List<Product>();

                while (reader.Read())
                {
                    Product currentProduct = new Product();
                    currentProduct.Product_ID = reader.GetInt32("Product_ID");
                    currentProduct.Name = reader.GetString("Name");
                    currentProduct.Calories = reader.GetDecimal("Calories");
                    currentProduct.Fat = reader.GetDecimal("Fat");
                    currentProduct.Carbohydrate = reader.GetDecimal("Carbohydrate");
                    currentProduct.Protien = reader.GetDecimal("Protien");

                    allProducts.Add(currentProduct);
                }
                return allProducts;
            }
        }
        public void AddProductToDatabase(Product newProduct)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO product (Name, Calories, Fat, Carbohydrate, Protien) " +
                              "VALUES (@Name, @Calories, @Fat, @Carbohydrate, @Protien);";
            cmd.Parameters.AddWithValue("Name", newProduct.Name);
            cmd.Parameters.AddWithValue("Calories", newProduct.Calories);
            cmd.Parameters.AddWithValue("Fat", newProduct.Fat);
            cmd.Parameters.AddWithValue("Carbohydrate", newProduct.Carbohydrate);
            cmd.Parameters.AddWithValue("Protien", newProduct.Protien);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
