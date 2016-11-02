using MvvmCross.Core.ViewModels;
using Glados.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Glados.Core.Interfaces;
using MvvmCross.Platform;
using SQLite;
using Glados.Core.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

/// <summary>
/// Author: Sathya Amarsee
/// Student Number: n9462716
/// Responsible Screen: Contacts Screen
/// Responsible Files: ContactsViewModel.cs, Contact.cs, ContactsLayout.axml, ContactsView.axml, ContactsView.cs
/// </summary>

namespace Glados.Core.ViewModels
{


    public class ContactsViewModel : MvxViewModel
    {
        private readonly IDatabase database;
       // private readonly IUserFavouritesStoreDatabase fav;


        UserStore loggedInUser;

        private ObservableCollection<IContactListType> contacts = new ObservableCollection<IContactListType>();
        private ObservableCollection<IContactListType> contactList = new ObservableCollection<IContactListType>();
        private ObservableCollection<IContactListType> filteredContacts = new ObservableCollection<IContactListType>();

        IEnumerable<UserStore> _contacts = new List<UserStore>();
        IEnumerable<UserStore> _contactList = new List<UserStore>();
        List<UserStore> _filteredContacts = new List<UserStore>();
        List<string> _favourites = new List<string>();
        public ObservableCollection<IContactListType> Contacts
        {
            get { return contacts; }
            set { SetProperty(ref contacts, value); }
        }
        public ObservableCollection<IContactListType> ContactList
        {
            get { return contactList; }
            set { SetProperty(ref contactList, value); }
        }
        public ObservableCollection<IContactListType> FilteredContacts
        {
            get { return filteredContacts; }
            set { SetProperty(ref filteredContacts, value); }
        }

        private string contactSearch;
        public string ContactSearch
        {
            get { return contactSearch; }
            set
            {
                SetProperty(ref contactSearch, value);
                if (String.IsNullOrEmpty(ContactSearch))
                {
                    Contacts.Clear();
                    GetContacts(false);
                }
                else if(contactSearch.Length > 0)
                {
                   SearchContacts(contactSearch);
                }
            }
        }

        public async Task<int> SearchContacts(string searchTerm)
        {
            _filteredContacts.Clear();
            _contacts = null;
            foreach(UserStore contact in _contactList)
            {
                //var cont = (ContactWrapper)contact;
                if(!_filteredContacts.Contains(contact) && contact.First_Name.ToLower().Contains(searchTerm))//First_Name.ToLower().Contains(searchTerm))
                {
                    _filteredContacts.Add(contact);
                }
            }

            Contacts.Clear();
            _contacts = _filteredContacts;
            /*foreach(UserStore con in _filteredContacts)
            {
                _contacts.Add(con);
            }*/
            GetContacts(false);
            return 1;
        }

        public async Task<int> favClick(Contact contact)
        {
            if (contact.IsFavourite)
            {
                contact.IsFavourite = false;
                var index = _favourites.IndexOf(contact.UserId);
                _favourites.Remove(contact.UserId);
                if (Contacts[0] is ContactLabel)
                {
                    index = index + 1;
                }
                Contacts.RemoveAt(index);              
                RaisePropertyChanged(() => Contacts);
                //Remove favourites heading if no favourites are there
                if (_favourites.Count <= 0 && ((Contacts[0] is ContactLabel) && (Contacts[0].Label == "Favourites")))
                {
                    Contacts.RemoveAt(0);
                }
                UserFavouritesStore favTemp = await database.GetFavourite(loggedInUser.Id, contact.UserId);

                favTemp.isFavourite = false;

                await database.UpdateFavourite(loggedInUser.Id, contact.UserId, false);
                System.Diagnostics.Debug.WriteLine("set to false: " + contact.IsFavourite);
                return 1;
            }
            else
            {
                contact.IsFavourite = true;
                //add favourites heading if not there
                if (!(Contacts[0] is ContactLabel) || (Contacts[0].Label != "Favourites"))
                {
                    Contacts.Insert(0, new ContactLabel("Favourites"));
                }
                if (!(_favourites.Contains(contact.UserId)))
                { 
                    _favourites.Add(contact.UserId);
                    System.Diagnostics.Debug.WriteLine("num in favourites list: " + _favourites.Count);
                    var index = _favourites.IndexOf(contact.UserId);

                    Contacts.Insert(index+1, new ContactWrapper(contact, this));
                    RaisePropertyChanged(() => Contacts);

                }

                if (await database.favouriteExists(loggedInUser.Id, contact.UserId))
                {
                    UserFavouritesStore favTemp = await database.GetFavourite(loggedInUser.Id, contact.UserId);

                    favTemp.isFavourite = true ;
                    await database.UpdateFavourite(loggedInUser.Id, contact.UserId, true);
                }
                else
                {
                    await database.InsertFavourite(loggedInUser.Id, contact.UserId, true);
                }

                System.Diagnostics.Debug.WriteLine("set to true: " + contact.IsFavourite);
                return 2;
            }
            return 0;
        }



        public async Task<int> Init(string currentUser)
        {
            loggedInUser = await database.GetSingleUser(currentUser);

            GetContacts(true);
            return 1;
        }

        public async void  GetContacts(bool getDatabase)
        {
            bool doesExist = false;
            if(getDatabase)
            {
                _contacts = await database.GetUsers(); /// need to add in wrapping for favourites and also separate details for groups
                _contactList = _contacts;
            }
            else
            {
                if (String.IsNullOrEmpty(ContactSearch))
                {   
                    _contacts = _contactList;
                }
                else
                {
                    _contacts = _filteredContacts;
                }
            }
            Contacts.Clear();
            _favourites.Clear();
            foreach (var user in _contacts)
            {
                if(!(user.Id == loggedInUser.Id)) { 
                    bool tempUserFav = false;
                    var User = new ContactWrapper(new Contact(user, tempUserFav), this);
                    if (String.IsNullOrEmpty(ContactSearch))
                    {
                        doesExist = await database.favouriteExists(loggedInUser.Id, user.Id);
                    }
                    //bool doesExist = await fav.favouriteExists(loggedInUser.Id, user.Id);
                    if (doesExist)
                    {
                        var favTemp = await database.GetFavourite(loggedInUser.Id, user.Id);
                        tempUserFav = favTemp.isFavourite;
                        if (tempUserFav && getDatabase || (tempUserFav && !getDatabase && String.IsNullOrEmpty(ContactSearch)))
                        {
                            User.Item.IsFavourite = true;
                            Contacts.Insert(_favourites.Count, User);
                            Debug.WriteLine("Added user"+user.Id+"to list");
                            _favourites.Add(user.Id);
                        }
                    }else if(!doesExist && getDatabase)
                    {
                        await database.InsertFavourite(loggedInUser.Id, user.Id, false);

                    }
                
                    Contacts.Add(User);
                        //ContactList.Add(User);
                }
            }
            //add headings for contacts and favourites sections
            if(getDatabase || (!getDatabase && String.IsNullOrEmpty(ContactSearch)))
            {
                Contacts.Insert(_favourites.Count, new ContactLabel("All Contacts"));
                if (_favourites.Count > 0)
                {
                    Contacts.Insert(0, new ContactLabel("Favourites"));
                }
            }
        }

        public void OnResume()
        {
            GetContacts(true);
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





        public ContactsViewModel(IDatabase database)
        {

            this.database = database;
            SelectContactCommandToast = new MvxCommand<ContactWrapper>( selectedContact => ShowViewModel<UserProfileViewModel>(selectedContact.Item));

            SelectContactCommandProfile = new MvxCommand<ContactWrapper>(selectedContact => ShowViewModel<SendMessageViewModel>( new { receiver =  selectedContact.Item.UserId , sender = loggedInUser.Id}));
            
            SwitchToMessages = new MvxCommand(()=> ShowViewModel<MessageViewModel>(
                new { currentUser = loggedInUser.Id }));

            ShowUserProfile = new MvxCommand(() => { profileCurrentUser();  } );

        }


        private void profileCurrentUser()
        {
            Contact user = new Contact(loggedInUser, false);

            ShowViewModel<UserProfileViewModel>(user);
        }
    }
}
