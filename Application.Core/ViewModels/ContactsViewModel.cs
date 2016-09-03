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
                new Contact("Alexander O.", false),
                new Contact("Alex M.", false),
                new Contact("Alex N.", false),
                new Contact("Barry M.", false),
                new Contact("Connor N.", false),
                new Contact("Jack H.", true),
                new Contact("Jake H.", false),
                new Contact("Jared B.", true),
                new Contact("Josh C.", false),
                new Contact("Josh R.", false),
                new Contact("Rachael F.", false),
                new Contact("Sathya A.", true),
                new Contact("Thomas D.", false),
            };
        }
    }
}
