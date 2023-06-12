using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.User
{
    public class UserUpdate
    {
        public int Id{get;set;}
        public string Username { get; set; }

        public string Password { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

    }
}