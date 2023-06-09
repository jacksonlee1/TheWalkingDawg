

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class DogsEntity
{
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int OwnerID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Breed { get; set; }

        [Required]
        public string? ReqDistance { get; set; }

        [Required]
        public int WalkingTime { get; set; }

        public string? SpecialRequests {get; set; }
    
}