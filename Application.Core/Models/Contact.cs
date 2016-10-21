using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class Contact : UserStore
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        new public string Email { get; set; }
        new public string Location { get; set; }
        new public string UserId { get; set; }

        private bool _isFavourite;
        public bool IsFavourite
        {
            get { return _isFavourite; }
            set
            {
                _isFavourite = value;
                RaisePropertyChanged(() => IsFavourite);
            }
        }
        public string ImagePath { get; set; }

        public Contact() { }
        public Contact(UserStore contact, bool isFav)
        {
            _isFavourite = isFav;
            FirstName = contact.First_Name;
            LastName = contact.Last_Name;
            Email = contact.Email;
            Location = contact.Location;
            UserId = contact.Id;
        }
    }
}
