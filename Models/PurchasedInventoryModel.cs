namespace DisasterAlleviation.Models
{
    public class PurchasedInventoryModel
    {
        public int id { get; set; }
        public int PurchaseGoodsID { get; set; }
        public string Category { get; set; }
        public int Noitems { get; set; }
        public string Username { get; set; }
    }
}
