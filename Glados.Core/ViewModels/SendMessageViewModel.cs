using MvvmCross.Core.ViewModels;
using Glados.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Glados.Core.Interfaces;
using System.Threading.Tasks;
using MvvmCross.Platform;
/// <summary>
/// Author: Jared Bagnall
/// Student Number: n5686245
/// Responsible Screen: Send Message Screen
/// Responsible Files: SendMessageViewModel.cs, SendMessage.axml, SendMessageView.cs
/// </summary>
namespace Glados.Core.ViewModels
{
    public class SendMessageViewModel : MvxViewModel
    {
        private readonly IDatabase database;
        private MessageRequestStore message;
         

        private UserStore loggedInUser;

        private UserStore selectedContact;
     
        public async Task<int> Init(string receiver, string sender)
        {
            selectedContact = await database.GetSingleUser(receiver);
            loggedInUser = await database.GetSingleUser(sender);
            Sender = loggedInUser.Id;
            UserName = selectedContact.Username;
            Receiver_First_Name = selectedContact.First_Name;
            Receiver = selectedContact.Id;
            return 1;
        }


        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _sender;

        public string Sender
        {
            get { return _sender; }
            set { SetProperty(ref _sender, value); }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }
        private string _time;

        public string Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }
        private string _meet;

        public string Meet
        {
            get { return _meet; }
            set { SetProperty(ref _meet, value); }
        }

        private string _meetingLocation;

        public string MeetingLocation
        {
            get { return _meetingLocation; }
            set { SetProperty(ref _meetingLocation, value); }
        }
        private string _receiver;

        public string Receiver
        {
            get { return _receiver; }
            set { SetProperty(ref _receiver, value); }
        }

        private string _receiver_first_name;

        public string Receiver_First_Name
        {
            get { return _receiver_first_name; }
            set { SetProperty(ref _receiver_first_name, value); }
        }

              
        /// <summary>
        /// Generates the message to be sent with the details tha have been entered
        /// </summary>
        private void  generateMessage()
        {
            SetLocation();
            SetMeeting();
            message = new MessageRequestStore();            
            message.ReceivedBy = this.Receiver;
            message.Sender = this.Sender;
            message.Location = this.Location;
            message.Meet = this.Meet;
            message.Time = this.Time;
            message.MeetingLocation = this.MeetingLocation;

        }


        public ICommand CreateMessage { get; private set; }

        public SendMessageViewModel(IDatabase database)
        {
            this.database = database;

            CreateMessage = new MvxCommand(() => {
                createNewMessage();
                 
            });
        }

        /// <summary>
        /// Processes the message into the database and directs to the message screen when successful
        /// </summary>
        /// <returns></returns>
        private async Task<int> createNewMessage()
        {
            generateMessage();
            var numNew = await database.InsertMessage(message);
            if (numNew > 0)
            {
                ShowViewModel<MessageViewModel>(new { currentUser = loggedInUser.Id });
                Mvx.Resolve<IToast>().Show("Message Sent!");
                return 1;
            }
            else
            {
                Mvx.Resolve<IToast>().Show("Send Error");
               
            }
            return 0;
        }


      
            private bool _locationSet;
            public bool LocationSet
        {
            get { return _locationSet; }
            set
            {
                SetProperty(ref _locationSet, value);
                SetLocation();
            }
        }

        private void SetLocation()
        {
            if (_locationSet)
            {
                _location = "Y";
            }
            else
            {
                _location = "N";
            }
        }

        private bool _meetingSet;
        public bool MeetingSet
        {
            get { return _meetingSet; }
            set
            {
                SetProperty(ref _meetingSet, value);
                SetMeeting();
            }
        }
        private void SetMeeting()
        {
            if (_meetingSet)
            {
                _meet = "Y";
            }
            else
            {
                _meet = "N";
            }
        }
    }
}

