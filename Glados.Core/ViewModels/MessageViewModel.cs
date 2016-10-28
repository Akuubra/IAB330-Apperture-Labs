using MvvmCross.Core.ViewModels;
using Glados.Core.Models;
using Glados.Core.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Platform;

/// <summary>
/// Author: Jack Hendy
/// Student Number: n9066845
/// Responsible Screen: Messaging Screen
/// Responsible Files: FirstViewModel.cs, Message.cs, MessageLayout.axml, FirstView.axml, FirstView.cs
/// </summary>
namespace Glados.Core.ViewModels
{
    public class MessageViewModel 
        : MvxViewModel
    {
        List<MessageRequestStore> rawMessages = new List<MessageRequestStore>();
        private ObservableCollection<MessageWrapper> messages = new ObservableCollection<MessageWrapper>();
        private ObservableCollection<MessageWrapper> filteredMessages = new ObservableCollection<MessageWrapper>();
        private readonly IMessageStoreDatabase messageStore;
        private readonly IUserStoreDatabase userStore;
       

        private UserStore loggedInUser;

        public async Task<int> Init(string currentUser)
        {
             loggedInUser = await userStore.GetSingleUser( currentUser);

           // loggedInUser = await userLoginDB.GetSingleUser(true);
            GetMessages();
            return 1;
        }
             

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


        //private async Task<UserStore> GetLoggedInUser()
        //{

        //    loggedInUser =  await Task.FromResult(await userStore.GetSingleUserByName("deraj"));
        //    GetMessages();
        //    return loggedInUser;
        //}
        private ObservableCollection<MessageWrapper> messageList;
        public ObservableCollection<MessageWrapper> MessageList
        {
            get { return messageList; }
            set { SetProperty(ref messageList, value);}
        }
        public ObservableCollection<MessageWrapper> Messages
        {
            get { return messages; }
            set { SetProperty(ref messages, value); }
        }
        public ObservableCollection<MessageWrapper> FilteredMessages
        {
            get { return FilteredMessages; }
            set { SetProperty(ref filteredMessages, value); }
        }
        private string _messageSearch;
        

        public string MessageSearch
        {
            get { return _messageSearch; }
            set
            {
                SetProperty(ref _messageSearch, value);
                if (string.IsNullOrEmpty(value))
                {
                    FilteredMessages = messages;
                    //SearchLocations(searchTerm);
                }
                else if(_messageSearch.Length > 3)
                {
                    SearchMessages(_messageSearch);
                    //return filtered list
                }
            }
        }
        
        
        public async void SearchMessages(string searchTerm)
        {

            foreach(MessageWrapper message in Messages)
            {
                if(message.MessageName == searchTerm)
                {
                    FilteredMessages.Add(message);
                }
            }

            //filteredmessages.add(message.where(filter));
            MessageList = FilteredMessages;
        }

        public ICommand SeeMessageDetails { get; private set; }
        public ICommand SwitchToContacts { get; private set; }
        public  MessageViewModel(IMessageStoreDatabase messageStore, IUserStoreDatabase userStore)
        {
            this.messageStore = messageStore;
            this.userStore = userStore;
            SwitchToContacts = new MvxCommand(() => ShowViewModel<ContactsViewModel>( new {  currentUser = loggedInUser.Id }));
            if(!(loggedInUser == null))
            {
                GetMessages();
            }
            SeeMessageDetails = new MvxCommand<MessageWrapper>(selectedMessage => {
                MessageViewSwitcher(selectedMessage);
            });

        }
        private void MessageViewSwitcher(MessageWrapper message)
        {
            if (message.GetMessage.Sender == loggedInUser.Id)
            {
                ShowViewModel<SenderMessageViewModel>(message.GetMessage);
            }
            else if (message.GetMessage.ReceivedBy == loggedInUser.Id)
            {

            }
            else
            {

                Mvx.Resolve<IToast>().Show("Error!");
            }

        }

    }
}
