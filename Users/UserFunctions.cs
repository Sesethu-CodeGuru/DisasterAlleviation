using System.Data.Common;
using System.Data.SqlClient;
namespace DisasterAlleviation.Users
{
    public class UserFunctions
    {
        static string connectionStr = Connection.Connect();

        public static bool FindUserByUsernameAndPassword(string Username, string Password)
        {
            bool success = false;
            string sqlstatement = $"SELECT * FROM {USERS.Table()} Where {USERS.Username()} =@Username " +
                $"and {USERS.Password()}=@Password";
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sqlstatement, connection);

                command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 8).Value = Username;
                command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 64).Value = Hash.C_MD5(Password);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            if (success)
            {
                LogIn(Username);
            }

            return success;
        }

        public static bool FindUserByUsername(string Username)
        {
            bool success = false;
            string sqlstatement = $"SELECT * FROM {USERS.Table()} Where {USERS.Username()} =@Username";
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sqlstatement, connection);

                command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 8).Value = Username;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return success;
        }

        public static bool AddUserByUsernameAndPassword(string Username, string Password)
        {
            bool success = false;
            string sqlstatement = @$"Insert into {USERS.Table()}({USERS.Username()},{USERS.Password()})"+
             $"values('{Username}','{Hash.C_MD5(Password)}')";
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sqlstatement, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (FindUserByUsernameAndPassword(Username, Password))
                    {
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return success;
        }

        public static bool Logged(string sql = "")
        {
            sql =
                $"select LL.{LOGGED.Username()} from {LOGGED.Table()} LL, {USERS.Table()} U " +
                $"where LL.{LOGGED.Username()}=U.{USERS.Username()} " +
                $"and LL.{LOGGED.Username()}=(select min(L.{LOGGED.Username()}) from {LOGGED.Table()} L)";

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

        public static string LoginUser()
        {
            string sql =
           $"select U.{LOGGED.Username()} from {LOGGED.Table()} LL, {USERS.Table()} U " +
           $"where LL.{LOGGED.Username()}=U.{USERS.Username()} and " +
           $"LL.{LOGGED.Username()}=(select min(L.{LOGGED.Username()}) from {LOGGED.Table()} L)";

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
                        value = string.Format("{0}", reader[USERS.Username()]);
                    }
                }

                connection.Close();
            }

            return value;
        }

        public static int LoginId()
        {
            string sql =
           $"select LL.{LOGGED.Username()} from {LOGGED.Table()} LL, {USERS.Table()} U " +
           $"where LL.{LOGGED.Username()}=U.{USERS.Username()} " +
           $"and LL.{LOGGED.Username()}=(select min(L.{LOGGED.Username()}) from {LOGGED.Table()} L)";
            int value = 0;
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
                        string output = string.Format("{0}", reader[USERS.Username()]);
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

        public static void LogOut()
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();

            using (command = new SqlCommand($"Delete from {LOGGED.Table()}", connection))
            {

                command.ExecuteNonQuery();

                connection.Close();

            }
        }

        public static void LogIn(string username)
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();

            using (command = new SqlCommand($"insert into {LOGGED.Table()}({LOGGED.Username()}) " +
                $"values((select {USERS.Username()} from {USERS.Table()} " +
                $"where {USERS.Username()}='{username}'))", connection))
            {

                command.ExecuteNonQuery();

                connection.Close();

            }
        }
    }
}
