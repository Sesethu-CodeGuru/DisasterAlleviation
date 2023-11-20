using DisasterAlleviation.Models;
using DisasterAlleviation.Users;
using System.Data.SqlClient;

namespace DisasterAlleviation.Inventory
{
    public class InventoryFunctions
    {
        static string connectionStr = Connection.Connect();
        public static double GetItems(int id)
        {
            string sql = $"select SUM({Inventory.Items()}) AS Total_Items  from {Inventory.Table()} where {Inventory.PGID()} = '{id}'";
            SqlDataReader reader;
            SqlCommand command;
            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();
            int value = 0;
            using (command = new SqlCommand(sql, connection))
            using (reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string output = string.Format("{0}", reader["Total_Items"]);
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

        public static string GetCategory(int id)
        {
            string sql = $"select {PurchaseGoods.PurchaseGoods.Cat()} from {PurchaseGoods.PurchaseGoods.Table()} where {PurchaseGoods.PurchaseGoods.ID()} = {id}";
            SqlDataReader reader;
            SqlCommand command;
            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();
            string value = "";
            using (command = new SqlCommand(sql, connection))
            using (reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        value = string.Format("{0}", reader[PurchaseGoods.PurchaseGoods.Cat()]);
                    }
                }

                connection.Close();
            }
            return value;
        }

        public static bool InventoryForeignPurchaseId(int id)
        {
            string sql = $"select * from {Inventory.Table()} where {Inventory.PGID()} = {id}";

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

        public static void AddToInventory(int id)
        {
            if (InventoryForeignPurchaseId(id))
            {
                //Increment the items of the purchased item
                string sql = $"UPDATE {Inventory.Table()} SET {Inventory.Items()} = {GetItems(id) + 1} WHERE {Inventory.PGID()} " +
                    $"= {id}";
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
                //Add a new record of the item
                string sql = $"insert into {Inventory.Table()}({Inventory.Username()},{Inventory.PGID()},{Inventory.PGCat()},{Inventory.Items()}) " +
                                    $"values('{UserFunctions.LoginUser()}',{id},'{GetCategory(id)}',1)";
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

        public static void GetInventory(out List<PurchasedInventoryModel> invent)
        {
            invent = new List<PurchasedInventoryModel>();

            string sql = $"select * from {Inventory.Table()}";

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
                            invent.Add(new PurchasedInventoryModel
                            {
                                id = int.Parse(string.Format("{0}", reader[Inventory.ID()])),
                                Username = string.Format("{0}", reader[Inventory.Username()]),
                                PurchaseGoodsID = int.Parse(string.Format("{0}", reader[Inventory.PGID()])),
                                Category = string.Format("{0}", reader[Inventory.PGCat()]),
                                Noitems = int.Parse(string.Format("{0}", reader[Inventory.Items()]))
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

        public static void GetInventory(int id, out PurchasedInventoryModel invent)
        {
            invent = new PurchasedInventoryModel();
            string sql = $"select * from {Inventory.Table()} where {Inventory.PGID()} = {id}";

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
                            invent.id = int.Parse(string.Format("{0}", reader[Inventory.ID()]));
                            invent.Username = string.Format("{0}", reader[Inventory.Username()]);
                            invent.PurchaseGoodsID = int.Parse(string.Format("{0}", reader[Inventory.PGID()]));
                            invent.Category = string.Format("{0}", reader[Inventory.PGCat()]);
                            invent.Noitems = int.Parse(string.Format("{0}", reader[Inventory.Items()]));
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
    }
}
