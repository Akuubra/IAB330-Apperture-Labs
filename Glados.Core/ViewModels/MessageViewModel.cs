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
        private ObservableCollection<MessageWrapper> messageList = new ObservableCollection<MessageWrapper>();
        private readonly IMessageStoreDatabase messageStore;
        private readonly IUserStoreDatabase userStore;
       

        private UserStore loggedInUser;

        public async Task<int> Init(string currentUser)
        {
             loggedInUser = await userStore.GetSingleUser( currentUser);

           // loggedInUser = await userLoginDB.GetSingleUser(true);
            await GetMessages();
            return 1;
        }

        private ObservableCollection<MessageWrapper> rawMessageList = new ObservableCollection<MessageWrapper>();
        public async Task<int> GetMessages()
        {
           
            var rawMessages = await messageStore.GetUsersMessages(loggedInUser.Id);
            rawMessageList.Clear();
            Messages.Clear();
            foreach (var message in rawMessages)
            {
                if (message.Sender == loggedInUser.Id)
                {
                    var receiver = await userStore.GetSingleUser(message.ReceivedBy);
                    rawMessageList.Add(new MessageWrapper(message, receiver.First_Name, true));
                }
                else
                {
                    var sender = await userStore.GetSingleUser(message.Sender);
                    rawMessageList.Add(new MessageWrapper(message, sender.First_Name, false));

                }
            }

            foreach(MessageWrapper message in rawMessageList)
            {
                Messages.Add(message);
                MessageList.Add(message);
                //FilteredMessages.Add(message);
            }
            
            //MessageList = RawMessageList;
            return 1;
        }


        //private async Task<UserStore> GetLoggedInUser()
        //{

        //    loggedInUser =  await Task.FromResult(await userStore.GetSingleUserByName("deraj"));
        //    GetMessages();
        //    return loggedInUser;
        //}
       
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
            get { return filteredMessages; }
            set { SetProperty(ref filteredMessages, value); }
        }
        private string _messageSearch;
        public string MessageSearch
        {
            get { return _messageSearch; }
            set
            {
                SetProperty(ref _messageSearch, value);
                if (String.IsNullOrEmpty(MessageSearch))
                {
                    Messages.Clear();
                    foreach(MessageWrapper message in MessageList)
                    {
                        Messages.Add(message);
                    }
                    //GetMessages();
                }
                else if (_messageSearch.Length > 0)
                {
                    SearchMessages(_messageSearch);
                }
            }
        }
        
        
        public async Task<int> SearchMessages(string searchTerm)
        {
            //await GetMessages();
            //rawMessageList.Clear();
            FilteredMessages.Clear();
            foreach (MessageWrapper message in Messages)
            {
                if(!MessageList.Contains(message))
                {
                    MessageList.Add(message);
                }
            }
            //Messages.Clear();
            //var _rawMessage = Messages;
            //_rawMessage.Clear();
            foreach (MessageWrapper mes in MessageList)
            {
                if(String.Equals(mes.MessageName,searchTerm,StringComparison.OrdinalIgnoreCase))
                {
                    FilteredMessages.Add(mes);
                    /*if(Messages.Contains(mes))
                    {
                        Messages.Clear();
                        Messages.Add(mes);
                    }
                    else
                    {
                        Messages.Add(mes);
                    }*/
                                //new MessageWrapper(mes.GetMessage, mes.MessageName, mes.GetMessage.Sender == loggedInUser.Id));
                }
            }
            Messages.Clear();
            foreach(MessageWrapper message in FilteredMessages)
            {
                Messages.Add(message);
            }
           // Messages.Clear();
          //  Messages = rawMessageList;
            return 1;
            //FilteredMessages.Clear();
        }

        public ICommand SeeMessageDetails { get; private set; }
        public ICommand SwitchToContacts { get; private set; }
        public  MessageViewModel(IMessageStoreDatabase messageStore, IUserStoreDatabase userStore)
        {
            this.messageStore = messageStore;
            this.userStore = userStore;
            SwitchToContacts = new MvxCommand(() => ShowViewModel<ContactsViewModel>( new {  currentUser = loggedInUser.Id }));

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
                ShowViewModel<ResponseMessageViewModel>(message.GetMessage);
            }
            else
            {

                Mvx.Resolve<IToast>().Show("Error!");
            }

        }

    }
}
