using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class Contact
    {
        public string ContactName { get; set; }
        public bool IsFavourite { get; set; }

        public Contact() { }
        public Contact(string contactName, bool isFavourite)
        {
            IsFavourite = isFavourite;
            ContactName = contactName;
        }
    }
}
