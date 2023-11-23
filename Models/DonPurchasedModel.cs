using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviation.Models
{
    public class DonPurchasedModel
    {
        public int id { get; set; }
        public DateTime Date { get; set; }
        public int DisasterID { get; set; }
        public string DisasterDescription { get; set; }
        public int Noitems { get; set; }
        public string Category { get; set; }
        public string Username { get; set; }
        public bool Anonymous { get; set; }
    }
}
