using System.ComponentModel.DataAnnotations;

namespace NapocaBike.Models
{
    public class ProposedBikeParking
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(250, ErrorMessage = "Name cannot be longer than 250 characters.")]
        [Display(Name = "Parking Name", Prompt = "Enter parking name...")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Latitude is required.")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        [Display(Name = "Latitude", Prompt = "Enter latitude...")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required.")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        [Display(Name = "Longitude", Prompt = "Enter longitude...")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
        [Display(Name = "Capacity", Prompt = "Enter capacity...")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Security Level is required.")]
        [Range(1, 5, ErrorMessage = "Security Level must be between 1 and 5.")]
        [Display(Name = "Security Level", Prompt = "Enter security level...")]
        public int SecurityLevel { get; set; }
        [Required]
        public bool IsApproved { get; set; }
    }
}
