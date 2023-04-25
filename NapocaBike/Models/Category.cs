namespace NapocaBike.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<LocationCategory>? LocationCategories { get; set; }
    }
}
