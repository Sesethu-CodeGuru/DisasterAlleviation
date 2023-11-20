using System;

namespace DisasterAlleviation.Models
{
    public class DonMonetaryModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int DisasterID { get; set; }
        public string DisasterDescription { get; set; }
        public double Amount { get; set; }
        public string Username { get; set; }
        public bool Anonymous { get; set; }
    }
}
