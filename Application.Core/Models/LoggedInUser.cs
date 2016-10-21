using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class LoggedInUser
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

        public bool LoggedIn { get; set; }

    }
}
