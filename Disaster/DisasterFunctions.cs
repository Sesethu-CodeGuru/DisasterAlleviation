using System.Data.SqlClient;
using DisasterAlleviation.Disaster;
using DisasterAlleviation.Goods;
using DisasterAlleviation.Models;

namespace DisasterAlleviation.Disaster
{
    public class DisasterFunctions
    {
        static string connectionStr = Connection.Connect();

        public void GetDisasters(out List<DisasterModel> Disasters)
        {
            Disasters = new List<DisasterModel>();

            string sql = $"select * from {Disaster.Disasters.Table()}";

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
                            Disasters.Add(new DisasterModel
                            {
                                ID = int.Parse(string.Format("{0}", reader[Disaster.Disasters.ID()])),
                                Startdate = DateTime.Parse(string.Format("{0}", reader[Disaster.Disasters.Start()])),
                                Enddate = DateTime.Parse(string.Format("{0}", reader[Disaster.Disasters.End()])),
                                Location = string.Format("{0}", reader[Disaster.Disasters.Location()]),
                                Description = string.Format("{0}", reader[Disaster.Disasters.Description()]),
                                Aidtype = string.Format("{0}", reader[Disaster.Disasters.AidTypes()])
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

        public DisasterModel GetDisaster(int? Id)
        {
            List<DisasterModel> Disasters = new List<DisasterModel>();

            string sql = $"select * from {Disaster.Disasters.Table()} where {Disaster.Disasters.ID()}={Id}";
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
                            Disasters.Add(new DisasterModel
                            {
                                ID = int.Parse(string.Format("{0}", reader[Disaster.Disasters.ID()])),
                                Startdate = DateTime.Parse(string.Format("{0}", reader[Disaster.Disasters.Start()])),
                                Enddate = DateTime.Parse(string.Format("{0}", reader[Disaster.Disasters.End()])),
                                Location = string.Format("{0}", reader[Disaster.Disasters.Location()]),
                                Description = string.Format("{0}", reader[Disaster.Disasters.Description()]),
                                Aidtype = string.Format("{0}", reader[Disaster.Disasters.AidTypes()])
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
            return Disasters[0];
        }

        public void AddDisaster(DisasterModel disaster)
        {
            DateTime startdate = DateTime.Parse(disaster.Startdate.ToString());
            DateTime enddate = DateTime.Parse(disaster.Enddate.ToString());
            string sql = $"insert into {Disasters.Table()}" +
                $"({Disasters.Start()},{Disasters.End()},{Disasters.Location()}," +
                $"{Disasters.Description()},{Disasters.AidTypes()}) " +
                $"values('{startdate.ToString("d")}','{enddate.ToString("d")}'," +
                $"'{disaster.Location}','{disaster.Description}','{disaster.Aidtype}')";

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
