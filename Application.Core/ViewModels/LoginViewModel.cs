using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using Application.Core.Models;
using System.Windows.Input;

namespace Application.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        //public ICommand Login { get; private set; }
        public void Login()
        {
            //Check User validity
            //if okay
                new MvxCommand<UserStore>(user => ShowViewModel<MessageViewModel>(user));
        }
        public LoginViewModel()
        {

             //Login = new MvxCommand<UserStore>(user => ShowViewModel<MessageViewModel>(user));
        }
    }
}
