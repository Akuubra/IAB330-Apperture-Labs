 using MvvmCross.Core.ViewModels;
using Application.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

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
        private UserStore user;

        private ObservableCollection<Message> messages;
        public ObservableCollection<Message> Messages
        {
            get { return messages; }
            set { SetProperty(ref messages, value); }
        }
        private string messageName;
        public string MessageName
        {
            get { return messageName; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref messageName, value);
                }
            }
        }
        private string messageContext;
        public string MessageContext
        {
            get { return messageContext; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref messageContext, value);
                }
            }
        }
        public ICommand SwitchToContacts { get; private set; }
        /*public void SwitchToContacts()
        {
            
        }*/
        //public ICommand SwitchToLogin { get; private set; }
        public MessageViewModel()
        {
            SwitchToContacts = new MvxCommand(() => ShowViewModel<ContactsViewModel>());
            
            //SwitchToLogin = new MvxCommand(() => ShowViewModel<LoginViewModel>());
            Messages = new ObservableCollection<Message>()
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
            };
        }

    }
}
