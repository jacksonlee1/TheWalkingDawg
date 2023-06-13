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
        public string Username { get; set; } 

        [Required]
        public string Password { get; set; } 

        [Required]
        public string PhoneNum { get; set; } 

        [Required]
        public string Address { get; set; } 

        [Required]
        public string Name {get;set;}        
        public virtual List<RatingEntity> Ratings {get; set;} =  new();
    }
}