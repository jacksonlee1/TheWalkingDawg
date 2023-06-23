using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Walks
{
    public class WalksUpdate
    {
        public int Id { get; set; }

        public double DistanceWalked { get; set; }

        public double Lattitude { get; set; }

        public double Longitude { get; set; }
        public int OutsideTemp { get; set; }

    }
}