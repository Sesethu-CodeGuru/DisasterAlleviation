using DisasterAlleviation.Disaster;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DisasterAlleviation.Monetary
{
    public class Monetary
    {
        public static string Table()
        {
            return "Monetary";
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
        public static string DATE()
        {
            return "DonationDate";
        }
        public static string AMOUNT()
        {
            return "DonationAmount";
        }
        public static string USERNAME()
        {
            return "DonationUsername";
        }
    }
}
