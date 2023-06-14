

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class DogsEntity
{
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }

        public virtual UserEntity Owner{get;set;}
        public virtual List<WalkingEntity> walks { get; set; } = new();

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

        [MaxLength(250)]
        public string? SpecialRequests {get; set; }
    
}