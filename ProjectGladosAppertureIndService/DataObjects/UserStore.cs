using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectGladosAppertureIndService.DataObjects
{
    public class UserStore : EntityData
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }
    }
}