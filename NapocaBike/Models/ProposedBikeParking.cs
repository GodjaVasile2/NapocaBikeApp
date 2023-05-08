using System.ComponentModel.DataAnnotations;

namespace NapocaBike.Models
{
    public class ProposedBikeParking : BikeParking
    {
        [Required]
        public bool IsApproved { get; set; }
    }
}
