using Glados.Core.Interfaces;
using Glados.Core.Models;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Glados.Core.ViewModels
{
    public class ResponseMessageViewModel : MvxViewModel
    {
        private readonly IUserStoreDatabase userStore;
        private readonly IMessageStoreDatabase messageStore;
        private readonly IMessageResponseStoreDatabase responseStore;
        private MessageRequestStore message;
        private MessageResponseStore messageResponse; 


        private UserStore loggedInUser;

        private UserStore selectedContact;

        public async Task<int> Init(MessageRequestStore message)
        {
            this.message = message;
            selectedContact = await userStore.GetSingleUser(message.Sender);
            loggedInUser = await userStore.GetSingleUser(message.ReceivedBy);
            Sender = loggedInUser.Id;
            UserName = selectedContact.Username;
            Receiver_First_Name = selectedContact.First_Name;
            Receiver = selectedContact.Id;
            Meet = message.Meet;
            MeetingSet = setter(message.Meet);
            Time = message.Time;

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



        private void CreateMessage()
        {
            messageResponse = new MessageResponseStore();
           // SetLocation();
            SetMeeting();
            //message = new MessageRequestStore();            
            messageResponse.MessageID = message.Id;
            messageResponse.Sender = message.ReceivedBy;
            messageResponse.Location = this.Location;
            messageResponse.Meet = this.Meet;
                
                
        }
        


        public ICommand RespondToMessage { get; private set; }
        

        public ResponseMessageViewModel(IUserStoreDatabase userStore, IMessageStoreDatabase messageStore, IMessageResponseStoreDatabase responseStore)
        {
            this.userStore = userStore;
            this.messageStore = messageStore;
            this.responseStore = responseStore;



            RespondToMessage = new MvxCommand(() => {
                UpdateMessage();
            });

        }

     

        private async Task<int> UpdateMessage()
        {
            CreateMessage();
          var numSent =  await responseStore.InsertMessage(messageResponse);
            //await messageStore.UpdateMessage(message);
            
            if (numSent>0)
            {
                ShowViewModel<MessageViewModel>(new { currentUser = loggedInUser.Id });
                Mvx.Resolve<IToast>().Show("Response Sent!");
            }else
            {
                Mvx.Resolve<IToast>().Show("Response Failed!");
            }
            
            return 1;
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

