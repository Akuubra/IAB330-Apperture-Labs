using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using Glados.Core.Models;
using System.Windows.Input;
using Glados.Core.Interfaces;
using MvvmCross.Platform;
using Microsoft.WindowsAzure.MobileServices;

namespace Glados.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        
        private readonly IDatabase UserDB;
        private string _userName;
        private UserStore tempUser;
        private readonly IDatabase userStore;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;

        public string Password {
            get { return _password; }
            set { SetProperty(ref _password, value); }
            }
        public ICommand Login { get; private set; }

        /// <summary>
        /// Logs in the user if the username and password match
        /// </summary>
        /// <returns></returns>
        public async Task<int> LoginUser()
        {
            
            tempUser = await userStore.GetUserLogin(_userName, _password);
            //Check User validity
            //if okay
            if (!(tempUser == null))
            {
                ShowViewModel<MessageViewModel>(new { currentUser = tempUser.Id });
            }
                else
            {
                Mvx.Resolve<IToast>().Show("Login Failed!");
            }
            return 1;
        }
        public ICommand CreateUser { get; private set; }
        public LoginViewModel(IDatabase userStore)
        {
            CreateUser = new MvxCommand(() => ShowViewModel<CreateUserViewModel>());
            this.userStore = userStore;

            Login = new MvxCommand(() =>
            {
            LoginUser();
               
            });
        }

        
    }
}
