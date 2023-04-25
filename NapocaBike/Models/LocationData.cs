namespace NapocaBike.Models
{
    public class LocationData
    {
        
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<LocationCategory> LocationCategories { get; set; }

    }
}
