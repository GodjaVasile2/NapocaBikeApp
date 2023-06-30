using System.ComponentModel.DataAnnotations;

namespace NapocaBike.Models
{
    public class Review
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The message field is required.")]
        public string Message { get; set; }

        public int MemberID { get; set; }
        public Member Member { get; set; }

        public int LocationID { get; set; }
        public Location Location { get; set; }
        
        public string MemberName { get; set; }
    }
}
