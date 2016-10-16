using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace Application.Core.Models
{
    public class UserStore
    {
        
        public string Id { get; set; }
        public string Username { get; set; }
        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }


    }
}
