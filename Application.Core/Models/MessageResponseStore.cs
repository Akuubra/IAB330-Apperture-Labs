using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class MessageResponseStore
    {
        public string Id { get; set; }

        public string MessageID { get; set; }
        public string Sender { get; set; }

        public string Location { get; set; }

        public string Meet { get; set; }
    }
}
