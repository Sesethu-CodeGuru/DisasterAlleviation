namespace DisasterAlleviation.Models
{
    public class DonGoodsModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Noitems { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public bool Anonymous { get; set; }
    }
}
