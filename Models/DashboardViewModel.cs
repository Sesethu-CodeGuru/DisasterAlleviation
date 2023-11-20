namespace DisasterAlleviation.Models
{
    public class DashboardViewModel
    {
        public double TotalMonetaryDonations { get; set; }
        public int TotalGoodsReceived { get; set; }
        public List<DisasterModel> ActiveDisasters { get; set; }
    }
}
