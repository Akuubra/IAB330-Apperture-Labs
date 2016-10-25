using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;


namespace Application.Core.ViewModels
{
    public class MainTabbedViewModel : MvxViewModel
    {
        private MessageViewModel _messageViewModel;
        public MessageViewModel MessageVM {
            get { return _messageViewModel; }
            set { SetProperty(ref _messageViewModel, value); }
        }

        private ContactsViewModel _contactsViewModel;
        public ContactsViewModel ContactsVM
        {
            get { return _contactsViewModel; }
            set { SetProperty(ref _contactsViewModel, value); }
        }

        
    }
}
