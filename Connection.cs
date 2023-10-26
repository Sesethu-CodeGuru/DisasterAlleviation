namespace DisasterAlleviation

{
    public class Connection
    {
        public static string Connect()
        {
            return "Server=tcp:djpromost10102571.database.windows.net,1433;Initial Catalog=DjPromoWebsite;Persist Security Info=False;User ID=djadmin;Password=Cheesylentic50;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}
