using NuGet.LibraryModel;
using System.Data.SqlClient;
using DisasterAlleviation.Users;
using DisasterAlleviation.Models;

namespace DisasterAlleviation.Wallet
{
    public class WalletFunctions
    {
        static string connectionStr = Connection.Connect();

        public static double GetWallet()
        {
            string sql = $"select {Wallet.AMOUNT()} from {Wallet.Table()} where {Wallet.Username()} = '{UserFunctions.LoginUser()}'";
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
                        string output = string.Format("{0}", reader[Wallet.AMOUNT()]);
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


        public static bool isWallet()
        {
            string sql = $"select {Wallet.AMOUNT()}  from  {Wallet.Table()}  where {Wallet.Username()}  = ' {UserFunctions.LoginUser()}'";

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

        public static void AddFunds(WalletModel Wallets)
        {
            if (isWallet())
            {
                string sql = $"UPDATE {Wallet.Table()} SET {Wallet.AMOUNT()} = {GetWallet() + Wallets.Amount} WHERE {Wallet.Username()} = '{UserFunctions.LoginUser()}'";
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
            else
            {
                string sql = $"insert into {Wallet.Table()}({Wallet.Username()},{Wallet.AMOUNT()}) " +
               $"values('{UserFunctions.LoginUser()}',{Wallets.Amount})";
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
