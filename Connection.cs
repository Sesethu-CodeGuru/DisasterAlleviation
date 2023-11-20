namespace DisasterAlleviation

{
    public class Connection
    {
        public static string Connect()
        {
            return "Server=tcp:sesethu-personalhp.database.windows.net,1433;Initial Catalog=test_database;Persist Security Info=False;User ID=Admin1;Password=Loginlogin1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}
