using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DisasterAlleviation.Disaster;
using DisasterAlleviation.Models;
using DisasterAlleviation.Users;

namespace DisasterAlleviation.Purchased
{
    public class PurchasedFunctions
    {
        static string connectionStr = Connection.Connect();

        public static void GetGoods(out List<DonPurchasedModel> Pgoods)
        {
            Pgoods = new List<DonPurchasedModel>();

            string sql = $"SELECT * FROM {Purchased.Table()}";

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                int noItems;
                                if (int.TryParse(reader[Purchased.Items()].ToString(), out noItems) && noItems > 0)
                                {
                                    Pgoods.Add(new DonPurchasedModel
                                    {
                                        id = Convert.ToInt32(reader[Purchased.ID()]),
                                        Date = Convert.ToDateTime(reader[Purchased.Date()]),
                                        DisasterID = Convert.ToInt32(reader[Purchased.DisasID()]),
                                        DisasterDescription = Convert.ToString(reader[Purchased.DisasDesc()]),
                                        Noitems = noItems,
                                        Username = Convert.ToString(reader[Purchased.Username()])
                                    });
                                }
                                else
                                {
                                    //case where the number of items is not a valid positive integer.
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }
                }
            }
        }

        public static void AddGoods(DonPurchasedModel goods)
        {
            string username = goods.Anonymous ? "anonymous" : UserFunctions.LoginUser();

            string sql = $"INSERT INTO {Purchased.Table()} " +
                         $"({Purchased.Date()}, {Purchased.DisasID()}, {Purchased.DisasDesc()}, " +
                         $"{Purchased.Items()}, {Purchased.Username()}) " +
                         $"VALUES(@Date, @DisasterID, @DisasterDesc, @Noitems, @Username)";

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Date", goods.Date);
                    command.Parameters.AddWithValue("@DisasterID", goods.DisasterID);
                    command.Parameters.AddWithValue("@DisasterDesc", GetDesc(goods.DisasterID));
                    command.Parameters.AddWithValue("@Noitems", goods.Noitems);
                    command.Parameters.AddWithValue("@Username", username);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static string GetDesc(int id)
        {
            string value = "";
            string sql = $"SELECT {Disasters.Description()} FROM {Disasters.Table()} WHERE {Disasters.ID()} = @ID";

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                value = Convert.ToString(reader[0]);
                            }
                        }
                    }
                }
            }

            return value;
        }
    }
}