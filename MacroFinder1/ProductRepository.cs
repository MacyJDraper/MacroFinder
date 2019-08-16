﻿using System;
using System.Collections.Generic;
using MacroFinder1.Models;
using MySql.Data.MySqlClient;
namespace MacroFinder1
{
    public class ProductRepository
    {

        private static string connectionString = System.IO.File.ReadAllText("ConnectionString.txt");

        public void DeleteProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM product WHERE Product_ID = @id";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public Product GetProduct(int id)
        {
            List<Product> results = getProducts(id);
            return results[0];
        }
        public List<Product> GetAllProducts()
        {
            return getProducts();
        }
        private List<Product> getProducts(int id = -1)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Product_ID, Name, Calories, Fat, Carbohydrate, Protien FROM product ";

            if(id != -1)
            {
                cmd.CommandText += "WHERE Product_ID = " + id;
            }

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
        public void UpdateProduct(Product updateProduct)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE product SET Name= @name, Calories= @calories, Fat= @fat, Carbohydrate= @carbohydrate, Protien= @protien WHERE Product_ID = @id;";
            cmd.Parameters.AddWithValue("Name", updateProduct.Name);
            cmd.Parameters.AddWithValue("Calories", updateProduct.Calories);
            cmd.Parameters.AddWithValue("Fat", updateProduct.Fat);
            cmd.Parameters.AddWithValue("Carbohydrate", updateProduct.Carbohydrate);
            cmd.Parameters.AddWithValue("Protien", updateProduct.Protien);
            cmd.Parameters.AddWithValue("id", updateProduct.Product_ID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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
        public List<Product> SearchProduct(IndexViewModel ivm)
        {
            int[] priorities = { 100, 50, 25, 12 }; 
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name, Calories, Fat, Carbohydrate, Protien, " +
                                "(abs(@TargetCal - Calories) / @TargetCal) * 100 + " +
                                "(abs(@TargetFat - Fat) / @TargetFat) * 50 + " +
                                "(abs(@TargetCarb - Carbohydrate) / @TargetCarb) * 25 + " +
                                "(abs(@TargetPro - Protien) / @TargetPro) * 12 " +
                                "as Score FROM MacroFinderDB.product ORDER BY score;";
            cmd.Parameters.AddWithValue("CalPriority", priorities[ivm.CalPriority - 1]);
            cmd.Parameters.AddWithValue("FatPriority", priorities[ivm.CalPriority - 1]);
            cmd.Parameters.AddWithValue("CarbPriority", priorities[ivm.CalPriority - 1]);
            cmd.Parameters.AddWithValue("ProPriority", priorities[ivm.CalPriority - 1]);

            cmd.Parameters.AddWithValue("TargetCal", ivm.TargetCal);
            cmd.Parameters.AddWithValue("TargetFat", ivm.TargetFat);
            cmd.Parameters.AddWithValue("TargetCarb", ivm.TargetCarb);
            cmd.Parameters.AddWithValue("TargetPro", ivm.TargetPro);

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                List<Product> product = new List<Product>();

                while (reader.Read())
                {
                    Product nextProduct = new Product();

                    nextProduct.Name = reader.GetString("Name");
                    nextProduct.Calories = reader.GetDecimal("Calories");
                    nextProduct.Fat = reader.GetDecimal("Fat");
                    nextProduct.Carbohydrate = reader.GetDecimal("Carbohydrate");
                    nextProduct.Protien = reader.GetDecimal("Protien");


                    product.Add(nextProduct);
                }
                return product;
            }
        }
    }
}
