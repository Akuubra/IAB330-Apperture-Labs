using MvvmCross.Core.ViewModels;
using Application.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Application.Core.Interfaces;
using MvvmCross.Platform;
using SQLite.Net;
using Application.Core.Database;
using System.Collections.Generic;

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
        private readonly IUserStoreDatabase database;

        private ObservableCollection<UserStore> contacts = new ObservableCollection<UserStore>();
        List<UserStore> _contacts = new List<UserStore>();
        public ObservableCollection<UserStore> Contacts
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

        public async void  GetContacts()
        {
            var _contacts = await database.GetUsers(); /// need to add in wrapping for favourites and also separate details for groups
            Contacts.Clear();
            foreach (var user in _contacts)
            {
                Contacts.Add(user);

            }
        }

        public void OnResume()
        {
            GetContacts();
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

        public ICommand SelectContactCommandProfile { get; private set; }
        public ICommand SelectContactCommandToast { get; private set; }
        public void SelectContactToast()
        {
            //actually send message
            Mvx.Resolve<IToast>().Show("Message Sent!");
        }
        public ICommand SwitchToMessages { get; private set; }
        public ICommand ShowUserProfile { get; private set; }
        public ContactsViewModel(IUserStoreDatabase contactDatbase)
        {
            //ShowUserPRofile = new MvxCommand(() => )
            database = contactDatbase;
            SelectContactCommandToast = new MvxCommand<UserStore>(selectedContact => ShowViewModel<UserProfileViewModel>(selectedContact));
            //()=> Mvx.Resolve<IToast>().Show("Message Sent!")

            SelectContactCommandProfile = new MvxCommand<UserStore>(selectedContact => ShowViewModel<SendMessageViewModel>(selectedContact));


            

            SwitchToMessages = new MvxCommand(()=> ShowViewModel<MessageViewModel>());
            

            GetContacts();
        }
    }
}
