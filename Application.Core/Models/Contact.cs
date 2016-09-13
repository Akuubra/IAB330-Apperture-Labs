using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class Contact
    {
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public bool IsFavourite { get; set; }

        public Contact() { }
        public Contact(string contactFirstName, string contactLastName, string contactEmail, bool isFavourite)
        {
            IsFavourite = isFavourite;
            ContactFirstName = contactFirstName;
            ContactLastName = contactLastName;
            ContactEmail = contactEmail;
        }
    }
}
