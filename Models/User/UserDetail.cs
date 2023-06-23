using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.User
{
    public class UserDetail
    {
        public string? Username{get;set;}
        public string? Name{get;set;}
        public int Id{get;set;}
        public string? Address {get;set;}
        public string? PhoneNum{get;set;}
        public double? AverageRating { get; set; }
    }
}