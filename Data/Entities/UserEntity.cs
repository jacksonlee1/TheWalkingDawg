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
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        public string Password { get; set; } 

        [Required]
        public string? FirstName { get; set; } 

        [Required]
        public string? LastName { get; set; } 
    }
}