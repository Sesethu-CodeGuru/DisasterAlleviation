using DisasterAlleviation.Models;
using DisasterAlleviation.Monetary;
using Microsoft.CodeAnalysis;
using System.Data.SqlClient;

namespace DisasterAlleviation.Category
{
    public class CategoryFunctions
    {
        static string connectionStr = Connection.Connect();

        public static void GetCatList(out List<GoodsCatModel> Category)
        {
            Category = new List<GoodsCatModel>();

            string sql = $"select * from {DisasterAlleviation.Category.Category.Table()}";

            SqlDataReader reader;
            SqlCommand command;
            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();

            using (command = new SqlCommand(sql, connection))
            using (reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            Category.Add(new GoodsCatModel
                            {
                                ID = int.Parse(string.Format("{0}", reader[DisasterAlleviation.Category.Category.ID()])),
                                Category = string.Format("{0}", reader[DisasterAlleviation.Category.Category.Cat()])
                            });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }

                connection.Close();
            }
        }

        public static bool CategoryExists(string Category)
        {
            return Exists($"select * from {DisasterAlleviation.Category.Category.Table()} where {DisasterAlleviation.Category.Category.Cat()}='{Category}'");
        }

        public static bool Exists(string sql)
        {
            Console.WriteLine(sql);
            SqlDataReader reader;
            SqlCommand command;
            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();

            using (command = new SqlCommand(sql, connection))
            using (reader = command.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }

            }
        }

        public static void AddCategory(string Category)
        {
            if (!CategoryExists(Category))
            {
                string sql = $"insert into {DisasterAlleviation.Category.Category.Table()}" +
                    $"({DisasterAlleviation.Category.Category.Cat()}) " +
                    $"values('{Category}')";
                Console.WriteLine(sql);
                SqlCommand command;
                SqlConnection connection = new SqlConnection(connectionStr);

                connection.Open();

                using (command = new SqlCommand(sql, connection))
                {

                    command.ExecuteNonQuery();

                    connection.Close();

                }
            }
        }

    }
}
