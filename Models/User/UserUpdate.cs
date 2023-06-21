using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.User
{
    public class UserUpdate
    {
        public int Id{get;set;}
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }= string.Empty;
        [Required]
        public string PhoneNum { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

    }
}