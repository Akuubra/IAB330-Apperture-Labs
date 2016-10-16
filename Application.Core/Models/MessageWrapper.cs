using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class MessageWrapper : MessageSentStore
    {
        public string MessageName { get; set; }
        public string MessageContext { get; set; }

        public MessageWrapper() { }
        public MessageWrapper(MessageSentStore message, string receiver, bool sender)
        {
            MessageName = receiver;
            if (sender)
            {
                if (message.Location == "Y" & message.Meet == "Y")
                {
                    MessageContext = string.Format("> Requested Location and Meet at {0}", message.Time);
                }else if (message.Location == "N" & message.Meet == "Y")
                {
                    MessageContext = string.Format("> Requested to Meet at {0}", message.Time);
                    
                }else
                {
                    MessageContext = string.Format("> Requested Location");
                }
            }
            else
            {
                if (message.Location == "Y" & message.Meet == "Y")
                {
                    MessageContext = string.Format("< Requested Location and Meet at {0}", message.Time);
                }
                else if (message.Location == "N" & message.Meet == "Y")
                {
                    MessageContext = string.Format("< Requested to Meet at {0}", message.Time);

                }
                else
                {
                    MessageContext = string.Format("< Requested Location");
                }
            }

        }
    }
}
