using MvvmCross.Core.ViewModels;
using Application.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Application.Core.Interfaces;
using System.Threading.Tasks;
using MvvmCross.Platform;
/// <summary>
/// Author: Jared Bagnall
/// Student Number: n5686245
/// Responsible Screen: Send Message Screen
/// Responsible Files: SendMessageViewModel.cs, SendMessage.axml, SendMessageView.cs
/// </summary>
namespace Application.Core.ViewModels
{
    public class SendMessageViewModel : MvxViewModel
    {
        private readonly IUserStoreDatabase userStore;
        private readonly IMessageStoreDatabase messageStore;
        private MessageSentStore message;
         

        private UserStore loggedInUser;

        private UserStore selectedContact;
        public void Init(UserStore parameters)
        {
            selectedContact = parameters;
            getLoggedInUser();
        }

        private async void getLoggedInUser()
        {
            loggedInUser = await userStore.GetSingleUserByName("deraj");
            _sender = loggedInUser.Id;
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


        public override void Start()
        {
            base.Start();
            _userName = selectedContact.Username;
            _receiver_first_name = selectedContact.First_Name; 
            _receiver = selectedContact.Id;

            //_sender = getUserDetails();
        }

        private string getUserDetails()
        {
            _sender  = this.loggedInUser.Id;
            return _sender;
        }

        private void  generateMessage()
        {
            message = new MessageSentStore();            
            message.ReceivedBy = this.Receiver;
            message.Sender = this.Sender;
            message.Location = this.Location;
            message.Meet = this.Meet;
            message.Time = this.Time;

        }


        public ICommand CreateMessage { get; private set; }

        public SendMessageViewModel(IUserStoreDatabase userStore, IMessageStoreDatabase messageStore)
        {
            this.userStore = userStore;
            this.messageStore = messageStore;
            //  SelectContactCommandToast = new MvxCommand(SelectContactToast);
            //()=> Mvx.Resolve<IToast>().Show("Message Sent!")

            CreateMessage = new MvxCommand(() => {
                createNewMessage();
                ShowViewModel<MessageViewModel>();
                Mvx.Resolve<IToast>().Show("Message Sent!");
            });
        }

        private async void createNewMessage()
        {
            generateMessage();
            //Debug.WriteLine(userTemp.First_Name.GetType());
            //Debug.WriteLine(userTemp.Last_Name.GetType());
            //Debug.WriteLine(userTemp.Username.GetType());
            //Debug.WriteLine(userTemp.Location.GetType());
            //Debug.WriteLine(userTemp.Email.GetType());
            await messageStore.InsertMessage(message);
        }


      
            private bool _locationSet;
            public bool LocationSet
            {
                get { return _locationSet; }
                set { SetProperty(ref _locationSet, value);
                    RaiseCanExecuteChanged(); }
            }

            private void RaiseCanExecuteChanged()
            {
                if (_locationSetCommand != null)
                _locationSetCommand.RaiseCanExecuteChanged();
            }

            private MvxCommand _locationSetCommand;
            public ICommand LocationSetCommand
            {
                get
                {
                _locationSetCommand = _locationSetCommand ?? new MvxCommand(DoMyCommand, CanProcessCommand);
                    return _locationSetCommand;
                }
            }

            private bool CanProcessCommand()
            {
                return LocationSet;
            }

        private void DoMyCommand()
        {
            Count++;
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value; RaisePropertyChanged(() => Count); }
        }
    }
}

