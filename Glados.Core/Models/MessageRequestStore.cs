using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Models
{
    public class MessageRequestStore
    {

        public string Id { get; set; }
        public string Sender { get; set; }

        public string ReceivedBy { get; set; }

        public string Location { get; set; }

        public string Time { get; set; }
        public string Meet { get; set; }
        public string MeetingLocation { get; set; }

        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
