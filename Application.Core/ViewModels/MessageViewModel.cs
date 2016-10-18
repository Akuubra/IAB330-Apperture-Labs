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
        List<MessageSentStore> rawMessages = new List<MessageSentStore>();
        private ObservableCollection<MessageWrapper> messages = new ObservableCollection<MessageWrapper>();

        private readonly IMessageStoreDatabase messageStore;
        private readonly IUserStoreDatabase userStore;

        private UserStore loggedInUser;


        //public void Init()
        //{
            
        //   // getLoggedInUser();
        //    GetMessages();
        //}

        public async void GetMessages()
        {
           
            var rawMessages = await messageStore.GetUsersMessages(loggedInUser.Id);
            
            Messages.Clear();
            foreach (var message in rawMessages)
            {
                if(message.Sender == loggedInUser.Id)
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

        //public async void GetDetails()

        public async  void OnResume()
        {
           await getLoggedInUser();
        }

        private async Task<UserStore> getLoggedInUser()
        {
            
            loggedInUser =  await Task.FromResult(await userStore.GetSingleUserByName("deraj"));
            GetMessages();
            return loggedInUser;
        }
        public ObservableCollection<MessageWrapper> Messages
        {
            get { return messages; }
            set { SetProperty(ref messages, value); }
        }
        //private string messageName;
        //public string MessageName
        //{
        //    get { return messageName; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            SetProperty(ref messageName, value);
        //        }
        //    }
        //}

        //private string messageContext;
        //public string MessageContext
        //{
        //    get { return messageContext; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            SetProperty(ref messageContext, value);
        //        }
        //    }
        //}


        public ICommand SwitchToContacts { get; private set; }
        public  MessageViewModel(IMessageStoreDatabase messageStore, IUserStoreDatabase userStore)
        {
            this.messageStore = messageStore;
            this.userStore = userStore;
            SwitchToContacts = new MvxCommand(() => ShowViewModel<ContactsViewModel>());
            getLoggedInUser() ;
             
            /*Messages = new ObservableCollection<Message>()
            {
                new Message("Jared", "^ Asked Location"),
                new Message("Sathya", "^ Asked to Meet: 10:30am"),
                new Message("John; Mary; Sam", "> Asked to Meet: 12:30am"),
                new Message("Jake", "> Asked Location"),
                new Message("Jake", "^ Asked to Meet: 1:30pm"),
                new Message("Jared", "^ Asked Location"),
                new Message("Sathya", "^ Asked to Meet: 10:30am"),
                new Message("John; Mary; Sam", "> Asked to Meet: 12:30am"),
                new Message("Jake", "> Asked Location"),
                new Message("Jake", "^ Asked to Meet: 1:30pm"),
                new Message("Jared", "^ Asked Location"),
                new Message("Sathya", "^ Asked to Meet: 10:30am"),
                new Message("John; Mary; Sam", "> Asked to Meet: 12:30am"),
                new Message("Jake", "> Asked Location"),
                new Message("Jake", "^ Asked to Meet: 1:30pm")
            };*/
        }

    }
}
