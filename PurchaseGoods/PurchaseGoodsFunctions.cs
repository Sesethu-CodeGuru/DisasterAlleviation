using DisasterAlleviation.Models;
using NuGet.LibraryModel;
using DisasterAlleviation.Wallet;
using DisasterAlleviation.Users;
using System.Data.SqlClient;

namespace DisasterAlleviation.PurchaseGoods
{
    public class PurchaseGoodsFunctions
    {
        static string connectionStr = Connection.Connect();

        public static void GetPurchase(out List<GoodsPurchaseCatModel> items)
        {
            items = new List<GoodsPurchaseCatModel>();

            string sql = $"select * from {PurchaseGoods.Table()}";

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
                            items.Add(new GoodsPurchaseCatModel
                            {
                                id = int.Parse(string.Format("{0}", reader[PurchaseGoods.ID()])),
                                Category = string.Format("{0}", reader[PurchaseGoods.Cat()]),
                                Price = float.Parse(string.Format("{0}", reader[PurchaseGoods.Price()]))
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

        public static double GetPrice(int id)
        {
            string sql = $"select {PurchaseGoods.Price()} from {PurchaseGoods.Table()} where {PurchaseGoods.ID()} = '{id}'";
            SqlDataReader reader;
            SqlCommand command;
            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();
            double value = 0;
            using (command = new SqlCommand(sql, connection))
            using (reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string output = string.Format("{0}", reader[PurchaseGoods.Price()]);
                        try
                        {
                            value = double.Parse(output);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }

                connection.Close();
            }
            return value;
        }

        public static void Withdraw(int id)
        {
                if (WalletFunctions.GetWallet() > GetPrice(id))
                {
                    string sql = $"UPDATE {Wallet.Wallet.Table()} SET {Wallet.Wallet.AMOUNT()} = {WalletFunctions.GetWallet() - GetPrice(id)} WHERE {Wallet.Wallet.Username()} = '{UserFunctions.LoginUser()}'";
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
