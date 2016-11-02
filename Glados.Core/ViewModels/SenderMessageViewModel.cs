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
/// Responsible Files: SenderMessageViewModel.cs, SenderMessage.axml, SenderMessageView.cs
/// </summary>
namespace Glados.Core.ViewModels
{
    public class SenderMessageViewModel : MvxViewModel
    {
        private readonly IDatabase database;
        private MessageResponseStore response;
        private MessageRequestStore message;
         

        private UserStore loggedInUser;

        private UserStore selectedContact;
     
        public async Task<int> Init(MessageRequestStore message)
        {
            this.message = message; 
            selectedContact = await database.GetSingleUser(message.ReceivedBy);
            loggedInUser = await database.GetSingleUser(message.Sender);
          
            Sender = loggedInUser.Id;
            UserName = selectedContact.Username;
            Receiver_First_Name = selectedContact.First_Name;
            Receiver = selectedContact.Id;
            Location = message.Location;
            LocationSet = setter(message.Location);
            Meet = message.Meet;
            MeetingSet = setter(message.Meet);
            Time = message.Time;
            MeetingLocation = message.MeetingLocation;
            ResponseReceived = await database.IsResponded(message.Id, message.ReceivedBy);

            if (ResponseReceived)
            {
                response = await database.GetResponse(message.Id, message.ReceivedBy);
                /// response details 
                /// 
                AcceptedMeeting = response.Meet == "Y" ? "I will attend" : "I can't attend";
                LocationReceived = response.Location;
          }
            


            return 1;
        }
      
        private bool setter(string indicator)
        {
            return indicator == "Y";
        }
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private bool _responseReceived;

        public bool ResponseReceived
        {
            get { return _responseReceived; }
            set { SetProperty(ref _responseReceived, value); }
        }
        private string _sender;

        public string Sender
        {
            get { return _sender; }
            set { SetProperty(ref _sender, value); }
        }

        private string _meetingLocation;

        public string MeetingLocation
        {
            get { return _meetingLocation; }
            set { SetProperty(ref _meetingLocation, value); }
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

        private string _locationReceived;
        public string LocationReceived {
            get { return _locationReceived; }
            set { SetProperty(ref _locationReceived, value); }
        }

        private string _acceptedMeeting;
        public string AcceptedMeeting
        {
            get { return _acceptedMeeting; }
            set { SetProperty(ref _acceptedMeeting, value); }
        }

        /// <summary>
        /// Sends an updated message for nudging and changing the update_at timestamp
        /// </summary>
        private void  NudgeMessage()
        {
            SetLocation();
            SetMeeting();
               
            message.ReceivedBy = this.Receiver;
            message.Sender = this.Sender;
            message.Location = this.Location;
            message.Meet = this.Meet;
            message.Time = this.Time;
            message.MeetingLocation = this.MeetingLocation;

        }

        /// <summary>
        /// Creates a blank message to be sent to ensure data is updates with update to update_at timestamp
        /// </summary>
        private void DumbMessage()
        {
            SetLocation();
            SetMeeting();
                 
            message.ReceivedBy = this.Receiver;
            message.Sender = this.Sender;
            message.Location = "";
            message.Meet = "";
            message.Time = "";
            message.MeetingLocation = "";

        }


        public ICommand NudgeThisMessage { get; private set; }

        public ICommand CancelThisMessage { get; private set; }

        public ICommand CompleteThisMessage { get; private set; }

        public SenderMessageViewModel(IDatabase database )
        {
            this.database = database;


            NudgeThisMessage = new MvxCommand(() => {
                UpdateMessage();
            });

            CancelThisMessage = new MvxCommand(() => {
                DeleteMessage();
               
            });

            CompleteThisMessage = new MvxCommand(() => {
                CompleteMessage();

            });
        }

        /// <summary>
        /// Deletes the message 
        /// </summary>
        /// <returns>value when successfull</returns>
        private async Task<int> DeleteMessage()
        {
            var delete = await database.DeleteMessage(message.Id);
            if(delete == 1)
            {
                ShowViewModel<MessageViewModel>(new { currentUser = loggedInUser.Id });
                Mvx.Resolve<IToast>().Show("Message Cancelled!");
            }
            else
            {
                Mvx.Resolve<IToast>().Show("Cancellation Failed!");
            }
        
            return 1;
        }

        /// <summary>
        /// Completes the message when requested
        /// </summary>
        /// <returns>returns value when successfull</returns>
        private async Task<int> CompleteMessage()
        {
            var delete = await database.DeleteMessage(message.Id);
            if (delete == 1)
            {
                ShowViewModel<MessageViewModel>(new { currentUser = loggedInUser.Id });
                Mvx.Resolve<IToast>().Show("Message Finalised.");
            }
            else
            {
                Mvx.Resolve<IToast>().Show("Finalisation Failed!");
            }

            return 1;
        }

        /// <summary>
        /// updates the message with the updated details. 
        /// </summary>
        /// <returns>int value when successful</returns>
        private async Task<int> UpdateMessage()
        {
            DumbMessage();

            await database.UpdateMessage(message);
            NudgeMessage();
            await database.UpdateMessage(message);


            ShowViewModel<MessageViewModel>(new { currentUser = loggedInUser.Id });
            Mvx.Resolve<IToast>().Show("Message Nudged!");
            return 1;
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

