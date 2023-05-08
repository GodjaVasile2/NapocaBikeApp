using System.ComponentModel.DataAnnotations;

namespace NapocaBike.Models
{
    public class Location
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string Program { get; set; }

        [Required(ErrorMessage = "The phone field is required.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The website field is required.")]
        [Url(ErrorMessage = "Invalid Website Address")]
        public string? Website { get; set; }

        [Display(Name = "Company Registration Number")]
        public string? CompanyRegistrationNumber { get; set; }

        public double? Latitude { get; set; }

  
        public double? Longitude { get; set; }

        public bool IsApproved { get; set; }

        public ICollection<LocationCategory>? LocationCategories { get; set; }
    }
}
