using System.ComponentModel.DataAnnotations;

namespace NapocaBike.Models
{
    public class BikeParking
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
        public int Capacity { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "SecurityLevel must be between 1 and 5.")]
        public int SecurityLevel { get; set; }
    }
}
