using System.Data.SqlClient;
using DisasterAlleviation.Disaster;
using DisasterAlleviation.Goods;
using DisasterAlleviation.Models;
using NuGet.LibraryModel;

namespace DisasterAlleviation.Disaster
{
    public class DisasterFunctions
    {
        static string connectionStr = Connection.Connect();

        public static void GetDisasters(out List<DisasterModel> Disasters)
        {
            Disasters = new List<DisasterModel>();

            string sql = $"SELECT * FROM {Disaster.Disasters.Table()}";

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
                            int disasterId = int.Parse(string.Format("{0}", reader[Disaster.Disasters.ID()]));

                            Disasters.Add(new DisasterModel
                            {
                                ID = disasterId,
                                Startdate = DateTime.Parse(string.Format("{0}", reader[Disaster.Disasters.Start()])),
                                Enddate = DateTime.Parse(string.Format("{0}", reader[Disaster.Disasters.End()])),
                                Location = string.Format("{0}", reader[Disaster.Disasters.Location()]),
                                Description = string.Format("{0}", reader[Disaster.Disasters.Description()]),
                                Aidtype = string.Format("{0}", reader[Disaster.Disasters.AidTypes()]),
                                AllocatedGoods = CalculateAllocatedGoodsForDisaster(disasterId)
                            });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }

            connection.Close();
        }

        public static int CalculateAllocatedGoodsForDisaster(int disasterId)
        {
            int allocatedGoods = 0;

            string sql = $"SELECT ISNULL(SUM({Purchased.Purchased.Items()}), 0) FROM {Purchased.Purchased.Table()} WHERE {Purchased.Purchased.DisasID()} = {disasterId}";

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        allocatedGoods = reader.IsDBNull(0) ? 0 : int.Parse(reader[0].ToString());
                    }
                }
            }

            return allocatedGoods;
        }

        public static DisasterModel GetDisaster(int? Id)
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
                                Aidtype = string.Format("{0}", reader[Disaster.Disasters.AidTypes()]),
                                AllocatedGoods = CalculateAllocatedGoodsForDisaster(int.Parse(string.Format("{0}", reader[Disaster.Disasters.ID()])))
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

        public static void AddDisaster(DisasterModel disaster)
        {
            DateTime startdate = disaster.Startdate;
            DateTime enddate = disaster.Enddate;

            string sql = $"INSERT INTO {Disasters.Table()} " +
                         $"({Disasters.Start()}, {Disasters.End()}, {Disasters.Location()}, " +
                         $"{Disasters.Description()}, {Disasters.AidTypes()}) " +
                         $"VALUES (@StartDate, @EndDate, @Location, @Description, @AidType)";

            SqlCommand command;
            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();

            using (command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@StartDate", startdate);
                command.Parameters.AddWithValue("@EndDate", enddate);
                command.Parameters.AddWithValue("@Location", disaster.Location);
                command.Parameters.AddWithValue("@Description", disaster.Description);
                command.Parameters.AddWithValue("@AidType", disaster.Aidtype);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public static string DisasterDesc(int id)
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
                        if (reader.HasRows && reader.Read())
                        {
                            value = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        }
                    }
                }
            }

            return value;
        }
    }
}
