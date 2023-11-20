using System.Data.SqlClient;
using DisasterAlleviation.Category;
using DisasterAlleviation.Disaster;
using DisasterAlleviation.Models;
using DisasterAlleviation.Users;
using NuGet.LibraryModel;

namespace DisasterAlleviation.Goods
{
    public class GoodsFunctions
    {
        static string connectionStr = Connection.Connect();

        public void GetDonations(out List<DonGoodsModel> Donations)
        {
            Donations = new List<DonGoodsModel>();

            string sql = $"select * from {Goods.Table()}";

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
                            Donations.Add(new DonGoodsModel
                            {
                                ID = int.Parse(string.Format("{0}", reader[Goods.ID()])),
                                Date = DateTime.Parse(string.Format("{0}", reader[Goods.Date()])),
                                DisasterID = int.Parse(string.Format("{0}", reader[Goods.DisasID()])),
                                DisasterDescription = string.Format("{0}", reader[Goods.DisasDesc()]),
                                Noitems = int.Parse(string.Format("{0}", reader[Goods.Items()])),
                                Category = string.Format("{0}", reader[Goods.Cat()]),
                                Description = string.Format("{0}", reader[Goods.Desc()]),
                                Username = string.Format("{0}", reader[Goods.Username()])
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

        public DonGoodsModel GetDonation(int? Id)
        {
            List<DonGoodsModel> Donation = new List<DonGoodsModel>();

            string sql = $"select * from {Goods.Table()} where {Goods.ID()}={Id}";

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
                            Donation.Add(new DonGoodsModel
                            {
                                ID = int.Parse(string.Format("{0}", reader[Goods.ID()])),
                                Date = DateTime.Parse(string.Format("{0}", reader[Goods.Date()])),
                                DisasterID = int.Parse(string.Format("{0}", reader[Goods.DisasID()])),
                                DisasterDescription = string.Format("{0}", reader[Goods.DisasDesc()]),
                                Noitems = int.Parse(string.Format("{0}", reader[Goods.Items()])),
                                Category = string.Format("{0}", reader[Goods.Cat()]),
                                Description = string.Format("{0}", reader[Goods.Desc()]),
                                Username = string.Format("{0}", reader[Goods.Username()])
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

        public void AddDonation(DonGoodsModel donation)
        {
            string username = (donation.Anonymous) ? "anonymous" : UserFunctions.LoginUser();
            DateTime date = DateTime.Parse(donation.Date.ToString());
            string category = Get($"select {Category.Category.Cat()} from {Category.Category.Table()} where {Category.Category.ID()}={int.Parse(donation.Category)}",0);
            Console.WriteLine("Text: "+donation.DisasterDescription);
            string sql = $"insert into {Goods.Table()}" +
                $"({Goods.DisasID()},{Goods.DisasDesc()},{Goods.Date()},{Goods.Items()},{Goods.Cat()}," +
                $"{Goods.Desc()},{Goods.Username()}) " +
                $"values({donation.DisasterID},'{GetDesc(donation.DisasterID)}','{date.ToString("s")}',{donation.Noitems},'{category}','{donation.Description}'," +
                $"'{username}')";

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

        public string Get(string sql, int coloumn_index)
        {
            string value = "";
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
                        string output = string.Format("{0}", reader[coloumn_index]);
                        value = output;
                    }
                }

                connection.Close();
            }

            return value;
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
