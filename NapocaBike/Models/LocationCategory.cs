namespace NapocaBike.Models
{
    public class LocationCategory
    {
        public int ID { get; set; }
        public int LocationID { get; set; }
        public Location Location { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
