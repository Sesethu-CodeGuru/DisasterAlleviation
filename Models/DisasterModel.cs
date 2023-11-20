namespace DisasterAlleviation.Models
{
    public class DisasterModel
    {
        public int ID { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Aidtype { get; set; }
        public int AllocatedGoods { get; set; }
    }
}
