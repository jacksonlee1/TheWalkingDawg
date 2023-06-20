using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Walks
{
    public class CreateWalks
    {
        [Required]
        public int DogId { get; set; }

        [Required]
        public double DistanceWalked { get; set; }

        [Required]
        public double Lattitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public int? WalkerId { get; set; }

        public int OutsideTemp { get; set; }

        public DateTime WalkStarted { get; set; } = DateTime.UnixEpoch;

        public DateTime WalkEnded { get; set; }
    }
}