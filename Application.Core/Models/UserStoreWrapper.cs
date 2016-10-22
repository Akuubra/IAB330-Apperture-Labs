using Application.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class UserStoreWrapper : UserStore
    {

        UserStore _contact;
        ContactsViewModel _parent;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
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


        public UserStoreWrapper() { }

        public UserStoreWrapper(UserStore contact, ContactsViewModel parent) {
            _contact = contact;
            _parent = parent;

            FirstName = _contact.First_Name;
            LastName = _contact.Last_Name;
            Email = _contact.Email;
            Location = _contact.Location;
        }


        public IMvxCommand tapFavourite
        {
            get
            {
                return new MvxCommand(() => _parent.favClick(new Contact()));
            }
        }

        public UserStore Item
        {
            get
            {
                return _contact;
            }
        }

    }
}
