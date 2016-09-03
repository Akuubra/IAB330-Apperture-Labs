using MvvmCross.Core.ViewModels;
using Application.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Application.Core.ViewModels
{
    public class ContactsViewModel : MvxViewModel
    {

        private ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Contacts
        {
            get { return contacts; }
            set { SetProperty(ref contacts, value); }
        }
        private string contactName;
        public string ContactName
        {
            get { return contactName; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref contactName, value);
                }
            }
        }

        public ContactsViewModel()
        {
            Contacts = new ObservableCollection<Contact>()
            {
                new Contact("Alex O."),
                new Contact("Alex N."),
                new Contact("Jared B."),
                new Contact("Jack H."),
                new Contact("Jake H."),
                new Contact("Sathya A."),

            };
        }
    }
}
