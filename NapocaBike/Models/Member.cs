using System.ComponentModel.DataAnnotations;

namespace NapocaBike.Models
{
    public class Member
    {
        public int ID { get; set; }

        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        public string? FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
        public string? LastName { get; set; }

        [StringLength(255, ErrorMessage = "Address cannot be longer than 255 characters.")]
        public string? Adress { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
        [Phone]
        public string? Phone { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string? ProfilePicturePath { get; set; }
    }
}
