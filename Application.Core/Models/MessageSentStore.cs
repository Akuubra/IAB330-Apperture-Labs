using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class MessageSentStore
    {

        public string Id { get; set; }
        public string Sender { get; set; }

        public string ReceivedBy { get; set; }

        public string Location { get; set; }

        public string Time { get; set; }
        public string Meet { get; set; }

        public string CreatedAt { get; set; } 
    }
}
