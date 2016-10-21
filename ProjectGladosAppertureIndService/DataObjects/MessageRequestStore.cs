using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectGladosAppertureIndService.DataObjects
{
    public class MessageRequestStore : EntityData
    {
        public string Sender { get; set; }

        public string ReceivedBy { get; set; }

        public string Location { get; set; }

        public string Time { get; set; }
        public string Meet { get; set; }
        public string MeetingLocation { get; set; }
    }
}