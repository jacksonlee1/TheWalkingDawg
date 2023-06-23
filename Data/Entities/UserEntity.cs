using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string PhoneNum { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public virtual List<RatingEntity> UserReviews { get; set; } = new();
        public virtual List<RatingEntity> Reviews { get; set; } = new();
        public virtual double AverageRating
        {
            get
            {

                if (Reviews.Count() == 0) return 0;

                var total = 0.0;
                foreach (var item in Reviews)
                {
                    total += item.Score;

                }

                return total / Reviews.Count();

            }
        }
        public virtual List<DogsEntity> Dogs { get; set; } = new();
        public virtual List<WalkingEntity> Walks { get; set; } = new();
    }
}