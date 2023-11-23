namespace DisasterAlleviation.Purchased
{
    public class Purchased
    {
        public static string Table()
        {
            return "PurchasedDonations";
        }
        public static string ID()
        {
            return "DonationID";
        }
        public static string DisasID()
        {
            return "DisasterID";
        }
        public static string DisasDesc()
        {
            return "DisasterDesc";
        }
        public static string Date()
        {
            return "DonationDate";
        }
        public static string Items()
        {
            return "DonationNoItems";
        }
        public static string Username()
        {
            return "DonationUsername";
        }
    }
}
