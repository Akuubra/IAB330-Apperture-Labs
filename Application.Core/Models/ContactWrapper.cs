using Application.Core.Interfaces;
using Application.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
   public class ContactWrapper : IContactListType
    {

        Contact _contact;
        ContactsViewModel _parent;
        public ContactWrapper() { }

        public ContactWrapper(Contact contact, ContactsViewModel parent)
        {
            _contact = contact;
            _parent = parent;
        }


        public IMvxCommand tapFavourite
        {
            get
            {
                return new MvxCommand(() => _parent.favClick(_contact));
            }
        }

        public Contact Item
        {
            get
            {
                return _contact;
            }
        }

    }
}
