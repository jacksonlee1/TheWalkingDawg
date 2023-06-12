using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Rating
{
    public class RatingDetail
    {
        public int WalkId { get; set; }
        public double Score { get; set; }
        public string? Comment { get; set; }
    }
}