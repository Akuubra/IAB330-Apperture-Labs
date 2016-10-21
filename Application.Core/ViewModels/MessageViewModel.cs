using MvvmCross.Core.ViewModels;
using Application.Core.Models;
using Application.Core.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Author: Jack Hendy
/// Student Number: n9066845
/// Responsible Screen: Messaging Screen
/// Responsible Files: FirstViewModel.cs, Message.cs, MessageLayout.axml, FirstView.axml, FirstView.cs
/// </summary>
namespace Application.Core.ViewModels
{
    public class MessageViewModel 
        : MvxViewModel
    {
        List<MessageRequestStore> rawMessages = new List<MessageRequestStore>();
        private ObservableCollection<MessageWrapper> messages = new ObservableCollection<MessageWrapper>();

        private readonly IMessageStoreDatabase messageStore;
        private readonly IUserStoreDatabase userStore;
        private readonly IUserLogin userLoginDB; 

        private LoggedInUser loggedInUser;

        public async Task<int> Init(string currentUser)
        {
            // loggedInUser = await userStore.GetSingleUser( currentUser);

            loggedInUser = await userLoginDB.GetSingleUser(true);
            GetMessages();
            return 1;
        }
             

        public async void GetMessages()
        {
           
            var rawMessages = await messageStore.GetUsersMessages(loggedInUser.UserId);
            
            Messages.Clear();
            foreach (var message in rawMessages)
            {
                if(message.Sender == loggedInUser.UserId)
                {
                    var receiver = await userStore.GetSingleUser(message.ReceivedBy);
                    Messages.Add(new MessageWrapper(message, receiver.First_Name, true));
                }
                else
                {
                    var sender = await userStore.GetSingleUser(message.Sender);
                    Messages.Add(new MessageWrapper(message, sender.First_Name, false));
                    
                }
                

            }
        }
        

        //private async Task<UserStore> GetLoggedInUser()
        //{
            
        //    loggedInUser =  await Task.FromResult(await userStore.GetSingleUserByName("deraj"));
        //    GetMessages();
        //    return loggedInUser;
        //}
        public ObservableCollection<MessageWrapper> Messages
        {
            get { return messages; }
            set { SetProperty(ref messages, value); }
        }



        public ICommand SwitchToContacts { get; private set; }
        public  MessageViewModel(IMessageStoreDatabase messageStore, IUserStoreDatabase userStore, IUserLogin userLoginDB)
        {
            this.userLoginDB = userLoginDB;
            this.messageStore = messageStore;
            this.userStore = userStore;
            SwitchToContacts = new MvxCommand(() => ShowViewModel<ContactsViewModel>( new {  currentUser = loggedInUser.UserId }));
            if(!(loggedInUser == null))
            {
                GetMessages();
            }
    
        }

    }
}
