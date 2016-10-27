using MvvmCross.Core.ViewModels;
using Application.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Application.Core.Interfaces;
using MvvmCross.Platform;
using SQLite;
using Application.Core.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

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
        private readonly IUserFavouritesStoreDatabase fav;


        UserStore loggedInUser;

        private ObservableCollection<IContactListType> contacts = new ObservableCollection<IContactListType>();
        List<UserStore> _contacts = new List<UserStore>();
        List<string> _favourites = new List<string>();
        public ObservableCollection<IContactListType> Contacts
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

        public async Task<int> favClick(Contact contact)
        {
            if (contact.IsFavourite)
            {
                contact.IsFavourite = false;
                var index = _favourites.IndexOf(contact.UserId);
                Contacts.RemoveAt(index+1);
                _favourites.RemoveAt(index);
                RaisePropertyChanged(() => Contacts);
                //Remove favourites heading if no favourites are there
                if (_favourites.Count <= 0)
                {
                    Contacts.RemoveAt(0);
                }
                UserFavouritesStore favTemp = await fav.GetFavourite(loggedInUser.Id, contact.UserId);

                favTemp.isFavourite = false;

                await fav.UpdateFavourite(loggedInUser.Id, contact.UserId, false);
                System.Diagnostics.Debug.WriteLine("set to false: " + contact.IsFavourite);
                return 1;
            }
            else
            {
                contact.IsFavourite = true;
                if (!(_favourites.Contains(contact.UserId)))
                    {
                    _favourites.Add(contact.UserId);
                    System.Diagnostics.Debug.WriteLine("num in favourites list: " + _favourites.Count);
                    var index = _favourites.IndexOf(contact.UserId);
                    if (_favourites.Count > 1)
                    {
                        index++;
                    }
                    Contacts.Insert(index, new ContactWrapper(contact, this));
                    RaisePropertyChanged(() => Contacts);

                    //add faovurites heading if not there
                    if (!(Contacts[0] is ContactLabel))
                    {
                        Contacts.Insert(0, new ContactLabel("Favourites"));
                    }
                }

                if (await fav.favouriteExists(loggedInUser.Id, contact.UserId))
                {
                    UserFavouritesStore favTemp = await fav.GetFavourite(loggedInUser.Id, contact.UserId);

                    favTemp.isFavourite = true ;
                    await fav.UpdateFavourite(loggedInUser.Id, contact.UserId, true);
                }
                else
                {
                    await fav.InsertFavourite(loggedInUser.Id, contact.UserId, true);
                }

                System.Diagnostics.Debug.WriteLine("set to true: " + contact.IsFavourite);
                return 2;
            }
            return 0;
        }



        public async Task<int> Init(string currentUser)
        {
            loggedInUser = await database.GetSingleUser(currentUser);

            GetContacts();
            return 1;
        }

        public async void  GetContacts()
        {
            var _contacts = await database.GetUsers(); /// need to add in wrapping for favourites and also separate details for groups
            Contacts.Clear();
            foreach (var user in _contacts)
            {
                bool tempUserFav = false;
                var User = new ContactWrapper(new Contact(user, tempUserFav), this);
                bool doesExist = await fav.favouriteExists(loggedInUser.Id, user.Id);
                if(doesExist)
                {
                    var favTemp = await fav.GetFavourite(loggedInUser.Id, user.Id);
                    tempUserFav = favTemp.isFavourite;
                    if (tempUserFav)
                    {
                        User.Item.IsFavourite = true;
                        Contacts.Insert(_favourites.Count, User);
                        Debug.WriteLine("Added user"+user.Id+"to list");
                        _favourites.Add(user.Id);
                    }
                }else
                {
                    await fav.InsertFavourite(loggedInUser.Id, user.Id, false);

                }
                
                Contacts.Add(User);

            }
            //add headings for contacts and favourites sections
            Contacts.Insert(_favourites.Count, new ContactLabel("All Contacts"));
            if (_favourites.Count > 0)
            {
                Contacts.Insert(0, new ContactLabel("Favourites"));
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





        public ContactsViewModel(IUserStoreDatabase contactDatbase, IUserFavouritesStoreDatabase fav)
        {
            //ShowUserPRofile = new MvxCommand(() => )
            this.fav = fav; 
            database = contactDatbase;
            SelectContactCommandToast = new MvxCommand<ContactWrapper>(
                selectedContact => 
                
                ShowViewModel<UserProfileViewModel>(
                    selectedContact.Item));
            //()=> Mvx.Resolve<IToast>().Show("Message Sent!")

            SelectContactCommandProfile = new MvxCommand<ContactWrapper>(selectedContact => ShowViewModel<SendMessageViewModel>( new { receiver =  selectedContact.Item.UserId , sender = loggedInUser.Id}));


            

            SwitchToMessages = new MvxCommand(()=> ShowViewModel<MessageViewModel>(
                new { currentUser = loggedInUser.Id }));
            

        }
    }
}
