using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class WalkingEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Dogs")]
        public int DogId { get; set; }

        public virtual DogsEntity? Dog { get; set;}

        [Required]
        public double DistanceWalked { get; set; }

        [Required]
        public double Lat { get; set; }

        [Required]
        public double Long { get; set; }

        [Required]
        public int? WalkerId { get; set; }
        
        public UserEntity? Walker{get;set;}

        public int OutsideTemp { get; set; }

        public DateTime WalkStarted { get; set; }

        public DateTime WalkEnded { get; set; }



    }
}