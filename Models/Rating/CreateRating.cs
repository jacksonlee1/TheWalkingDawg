using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Rating
{
    public class CreateRating
    {
        [Required]
        public int WalkId { get; set; }
        [Required]
        public int WalkerId{get;set;}
        [Required]
        public double Score { get; set; }
        public string? Comment { get; set; }
    }
}