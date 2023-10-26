using System.Data.SqlClient;
using System.Reflection;
using DisasterAlleviation.Models;
using DisasterAlleviation.Users;

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
                $"({Monetary.DATE()},{Monetary.AMOUNT()},{Monetary.USERNAME()}) " +
                $"values('{date.ToString("d")}',{donation.Amount},'{username}')";
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
            SqlDataReader reader = null;
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

        public int Get(string sql, string coloumn_name)
        {
            int value = -1;
            SqlDataReader reader = null;
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
                        string output = string.Format("{0}", reader[0]);
                        try
                        {
                            value = int.Parse(output);
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

        public void Run(string sql)
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();

            using (command = new SqlCommand(sql, connection))
            {

                command.ExecuteNonQuery();

                connection.Close();

            }
        }

        int SemesterWeeks()
        {
            string FTable = "MODULE_DETAILS";
            string STable = "USER_LoggedIn";
            string TTable = "USER_MODULES";
            string FrTable = "USER_SEMESTER";
            string FvTable = "SEMESTER_DETAILS";
            string Column = "USERNAME";
            string Column1 = "MODULE_ID";

            int weeks = Get($"select s.SEMESTER_WEEKS from {FvTable} s,{FrTable} us,{TTable} um,{STable} L,{FTable} m" +
               $" where s.SEMESTER_ID = us.SEMESTER_ID and us.{Column} = um.{Column} and um.{Column} = L.{Column} and  um.{Column1}=m.{Column1}", "s.SEMESTER_WEEKS");
            return weeks;
        }

    }
}
