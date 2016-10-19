using MvvmCross.Core.ViewModels;
using Application.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Application.Core.Interfaces;
using MvvmCross.Platform;

/// <summary>
/// Author: Sathya Amarsee
/// Student Number: n9462716
/// Responsible Screen: Contacts Screen
/// Responsible Files: ContactsViewModel.cs, Contact.cs, ContactsLayout.axml, ContactsView.axml, ContactsView.cs
/// </summary>

namespace Application.Core.ViewModels
{


    public class ContactsViewModel : MvxViewModel
    {

        private ObservableCollection<ContactsWrapper> contacts;
        public ObservableCollection<ContactsWrapper> Contacts
        {
            get { return contacts; }
            set { SetProperty(ref contacts, value); }
        }

        private string contactSearch;
        public string ContactSearch
        {
            get { return contactSearch; }
            set
            {
                SetProperty(ref contactSearch, value);
                if (contactSearch.Length > 3)
                {

                    //SearchLocations(searchTerm);
                }
            }
        }
        /*private string contactFirstName;
        public string ContactFirstName
        {
            get { return contactFirstName; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref contactFirstName, value);
                }
            }
        }*/

        public void favClick(Contact contact)
        {
            if(contact.IsFavourite)
            {
                contact.IsFavourite = false;
                System.Diagnostics.Debug.WriteLine("set to false: "+ contact.IsFavourite);
            } else
            {
                contact.IsFavourite = true;
                System.Diagnostics.Debug.WriteLine("set to true: " + contact.IsFavourite);
            }

        }


        public ICommand SelectContactCommandProfile { get; private set; }
        public ICommand SelectContactCommandToast { get; private set; }
        public void SelectContactToast()
        {
            //actually send message
            Mvx.Resolve<IToast>().Show("Message Sent!");
        }
        public ICommand SwitchToMessages { get; private set; }
        public ContactsViewModel()
        {
            SelectContactCommandToast = new MvxCommand(SelectContactToast);
            //()=> Mvx.Resolve<IToast>().Show("Message Sent!")

            SelectContactCommandProfile = new MvxCommand<Contact>(selectedContact => ShowViewModel<UserProfileViewModel>(selectedContact));
            SwitchToMessages = new MvxCommand(()=> ShowViewModel<FirstViewModel>());
            Contacts = new ObservableCollection<ContactsWrapper>()
            {
                new ContactsWrapper(new Contact("Alexander", "Henry", "A.Henry@gmail.com", true), this),
                new ContactsWrapper(new Contact("Alex", "Manderson", "A.Manderson@gmail.com", false), this),
                new ContactsWrapper(new Contact("Alex", "Nelly", "A.Nelly@gmail.com", false), this),
                new ContactsWrapper(new Contact("Barry", "Mitchel", "B.Mitchel@gmail.com", false), this),
                /*new Contact("Jake H.", false),
                new Contact("Jared B.", true),
                new Contact("Josh C.", false),
                new Contact("Josh R.", false),
                new Contact("Rachael F.", false),
                new Contact("Sathya A.", true),
                new Contact("Thomas D.", false),*/
            };
        }
    }
}
