using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class Message
    {
        public string MessageName { get; set; }
        public string MessageContext { get; set; }

        public Message() { }
        public Message(string messageName, string messageContext)
        {
            MessageName = messageName;
            MessageContext = messageContext;
        }
    }
}