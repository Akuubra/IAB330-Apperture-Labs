using Glados.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Models
{
    public class ContactLabel : IContactListType
    {
        public ContactLabel() { }

        public ContactLabel(string label)
        {
            Label = label;
        }


        public string Label { get; set; }
    }
}
