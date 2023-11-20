using System.Data.SqlClient;
using System.Reflection;
using DisasterAlleviation.Disaster;
using DisasterAlleviation.Models;
using DisasterAlleviation.Users;
using NuGet.LibraryModel;

namespace DisasterAlleviation.Monetary
{
    public class MonetaryFunctions
    {
        static string connectionStr = Connection.Connect();

        public void GetDonations(out List<DonMonetaryModel> Donations)
        {
            Donations = new List<DonMonetaryModel>();

            string sql = $"select * from {Monetary.Table()}";

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
                            Donations.Add(new DonMonetaryModel { 
                                ID = int.Parse(string.Format("{0}", reader[Monetary.ID()])),
                                DisasterID = int.Parse(string.Format("{0}", reader[Monetary.DisasID()])),
                                DisasterDescription = string.Format("{0}", reader[Monetary.DisasDesc()]),
                                Date = DateTime.Parse(string.Format("{0}", reader[Monetary.DATE()])),
                                Amount = double.Parse(string.Format("{0}", reader[Monetary.AMOUNT()])),
                                Username = string.Format("{0}", reader[Monetary.USERNAME()])
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

        public DonMonetaryModel GetDonation(int? Id)
        {
            List<DonMonetaryModel> Donation = new List<DonMonetaryModel>();

            string sql = $"select * from {Monetary.Table()} where {Monetary.ID()}={Id}";
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
                    while (reader.Read())
                    {
                        try
                        {
                            Donation.Add(new DonMonetaryModel
                            {
                                ID = int.Parse(string.Format("{0}", reader[Monetary.ID()])),
                                DisasterID = int.Parse(string.Format("{0}", reader[Monetary.DisasID()])),
                                DisasterDescription = string.Format("{0}", reader[Monetary.DisasDesc()]),
                                Date = DateTime.Parse(string.Format("{0}", reader[Monetary.DATE()])),
                                Amount = double.Parse(string.Format("{0}", reader[Monetary.AMOUNT()])),
                                Username = string.Format("{0}", reader[Monetary.USERNAME()])
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
            return Donation[0];
        }

        public void AddDonation(DonMonetaryModel donation)
        {
            string username = (donation.Anonymous)?"anonymous":UserFunctions.LoginUser();
            DateTime date = DateTime.Parse(donation.Date.ToString());
            string sql = $"insert into {Monetary.Table()}" +
                $"({Monetary.DisasID()},{Monetary.DisasDesc()},{Monetary.DATE()},{Monetary.AMOUNT()},{Monetary.USERNAME()}) " +
                $"values({donation.DisasterID},'{GetDesc(donation.DisasterID)}','{date.ToString("s")}',{donation.Amount},'{username}')";
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

        public string GetDesc(int id)
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
