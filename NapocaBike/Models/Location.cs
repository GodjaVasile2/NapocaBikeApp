using System.ComponentModel.DataAnnotations;

namespace NapocaBike.Models
{
    public class Location
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string Program { get; set; }


        [Required(ErrorMessage = "Latitude is required")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double? Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double? Longitude { get; set; }


        public ICollection<LocationCategory>? LocationCategories { get; set; }
    }
}
