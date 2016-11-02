using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Models
{
    public class MessageWrapper : MessageRequestStore
    {
        MessageRequestStore message;
        public string MessageName { get; set; }
        public string MessageContext { get; set; }
        public bool MessageSender { get; set; }
        public bool ResponseReceived { get; set; }



        public MessageWrapper() { }
        public MessageWrapper(MessageRequestStore message, string receiver, bool sender, bool responseReceived)
        {
            ResponseReceived = responseReceived;
            MessageSender = sender;
            this.message = message;
            MessageName = receiver;

            /// adds specific message context bases on whether a meeting and/or location has been requesed
            if (sender)
            {
                if (message.Location == "Y" & message.Meet == "Y")
                {
                    MessageContext = string.Format("Requested their location and meet them at {0} in {1}", message.Time, message.MeetingLocation);
                }
                else if (message.Location == "N" & message.Meet == "Y")
                {
                    MessageContext = string.Format("Requested to meet them at {0} in {1}", message.Time, message.MeetingLocation);

                }
                else
                {
                    MessageContext = string.Format("Requested their Location");
                }
            }
            else
            {
                if (message.Location == "Y" & message.Meet == "Y")
                {
                    MessageContext = string.Format("Requested your location and meet you at {0} in {1}", message.Time, message.MeetingLocation);
                }
                else if (message.Location == "N" & message.Meet == "Y")
                {
                    MessageContext = string.Format("Requested to meet you at {0} in {1}", message.Time, message.MeetingLocation);

                }
                else
                {
                    MessageContext = string.Format("Requested your location");
                }
            }

        }

        public MessageRequestStore GetMessage
        {
            get { return message;  }

        }
    }
}
