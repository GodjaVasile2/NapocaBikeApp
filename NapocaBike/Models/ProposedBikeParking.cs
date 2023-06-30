using System.ComponentModel.DataAnnotations;

namespace NapocaBike.Models
{
    public class ProposedBikeParking
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int SecurityLevel { get; set; }

        [Required]
        public bool IsApproved { get; set; }
    }
}
