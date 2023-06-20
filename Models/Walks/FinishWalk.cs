using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Walks
{
    public class FinishWalk
    {
        public int Id { get; set; }
        public int DogId { get; set; }

        public double DistanceWalked { get; set; }

        public double Lattitude { get; set; }

        public double Longitude { get; set; }

        public string WalkerName { get; set; }

        public int OutsideTemp { get; set; }

        public DateTime WalkStarted { get; set; }

        public DateTime WalkEnded { get; set; } 

        public string DogName { get; set; }
    }
}