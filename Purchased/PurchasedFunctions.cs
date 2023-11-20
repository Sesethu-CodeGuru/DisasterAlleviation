using DisasterAlleviation.Models;
using DisasterAlleviation.Users;
using Microsoft.CodeAnalysis;
using NuGet.LibraryModel;
using System.Data.SqlClient;
using DisasterAlleviation.Disaster;

namespace DisasterAlleviation.Purchased
{
    public class PurchasedFunctions
    {
        static string connectionStr = Connection.Connect();
        public static void GetGoods(out List<DonPurchasedModel> Pgoods)
        {
            Pgoods = new List<DonPurchasedModel>();

            string sql = $"select * from {Purchased.Table()}";

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
                            Pgoods.Add(new DonPurchasedModel
                            {
                                id = int.Parse(string.Format("{0}", reader[Purchased.ID()])),
                                Date = DateTime.Parse(string.Format("{0}", reader[Purchased.Date()])),
                                DisasterID = int.Parse(string.Format("{0}", reader[Purchased.DisasID()])),
                                DisasterDescription = string.Format("{0}", reader[Purchased.DisasDesc()]),
                                Noitems = int.Parse(string.Format("{0}", reader[Purchased.Items()])),
                                Category = string.Format("{0}", reader[Purchased.Cat()]),
                                Username = string.Format("{0}", reader[Purchased.Username()])
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
        public static void AddGoods(DonPurchasedModel goods)
        {
            string username = (goods.Anonymous) ? "anonymous" : UserFunctions.LoginUser();
            DateTime date = DateTime.Parse(goods.Date.ToString());
            string sql = $"insert into {Purchased.Table()}({Purchased.Date()},{Purchased.DisasID()}," +
                $"{Purchased.DisasDesc()},{Purchased.Items()},{Purchased.Cat()},{Purchased.Username()}) " +
                $"values('{date.ToString("s")}',{goods.DisasterID},'{GetDesc(goods.DisasterID)}','{goods.Noitems}','{goods.Category}','{username}')";

            SqlCommand command;
            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();

            using (command = new SqlCommand(sql, connection))
            {

                command.ExecuteNonQuery();

                connection.Close();

            }
        }
        public static string GetDesc(int id)
        {
            string value = "";
            SqlDataReader reader;
            SqlCommand command;
            string sql = $"select {Disasters.Description()} from {Disasters.Table()} where {Disasters.ID()} ={id}";

            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();

            using (command = new SqlCommand(sql, connection))
            using (reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        string output = string.Format("{0}", reader[0]);
                        value = output;
                    }
                }

                connection.Close();
            }

            return value;
        }
    }
}
