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
    public class SenderMessageViewModel : MvxViewModel
    {
        private readonly IUserStoreDatabase userStore;
        private readonly IMessageStoreDatabase messageStore;
        private readonly IMessageResponseStoreDatabase responseStore;
        private MessageResponseStore response;
        private MessageRequestStore message;
         

        private UserStore loggedInUser;

        private UserStore selectedContact;
     
        public async Task<int> Init(MessageRequestStore message)
        {
            this.message = message; 
            selectedContact = await userStore.GetSingleUser(message.ReceivedBy);
            loggedInUser = await userStore.GetSingleUser(message.Sender);
          
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
            ResponseReceived = await responseStore.IsResponded(message.Id, message.ReceivedBy);

            if (ResponseReceived)
            {
                response = await responseStore.GetResponse(message.Id, message.ReceivedBy);
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


        private void  NudgeMessage()
        {
            SetLocation();
            SetMeeting();
            //message = new MessageRequestStore();            
            message.ReceivedBy = this.Receiver;
            message.Sender = this.Sender;
            message.Location = this.Location;
            message.Meet = this.Meet;
            message.Time = this.Time;
            message.MeetingLocation = this.MeetingLocation;

        }

        private void DumbMessage()
        {
            SetLocation();
            SetMeeting();
            //message = new MessageRequestStore();            
            message.ReceivedBy = this.Receiver;
            message.Sender = this.Sender;
            message.Location = "";
            message.Meet = "";
            message.Time = "";
            message.MeetingLocation = "";

        }


        public ICommand NudgeThisMessage { get; private set; }

        public ICommand CancelThisMessage { get; private set; }

        public SenderMessageViewModel(IUserStoreDatabase userStore, IMessageStoreDatabase messageStore, IMessageResponseStoreDatabase responseStore)
        {
            this.responseStore = responseStore;
            this.userStore = userStore;
            this.messageStore = messageStore;
            //  SelectContactCommandToast = new MvxCommand(SelectContactToast);
            //()=> Mvx.Resolve<IToast>().Show("Message Sent!")

            NudgeThisMessage = new MvxCommand(() => {
                UpdateMessage();
            });

            CancelThisMessage = new MvxCommand(() => {
                DeleteMessage();
               
            });
        }

        private async Task<int> DeleteMessage()
        {
            var delete = await messageStore.DeleteMessage(message.Id);
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

        private async Task<int> UpdateMessage()
        {
            DumbMessage();

            await messageStore.UpdateMessage(message);
            NudgeMessage();
            await messageStore.UpdateMessage(message);


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

