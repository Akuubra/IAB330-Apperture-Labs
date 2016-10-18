using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using Application.Core.Models;
using System.Windows.Input;
using Application.Core.Interfaces;
using MvvmCross.Platform;

namespace Application.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private string _userName;
        private UserStore tempUser;
        private readonly IUserStoreDatabase userStore;
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
        public async Task<int> LoginUser()
        {
            tempUser = await userStore.GetUserLogin(_userName, _password);
            //Check User validity
            //if okay
            if (!(tempUser == null))
            {
                ShowViewModel<MessageViewModel>(tempUser);
            }else
            {
                Mvx.Resolve<IToast>().Show("Login Failed!");
            }
            return 1;
        }
        public ICommand CreateUser { get; private set; }
        public LoginViewModel(IUserStoreDatabase userStore)
        {
            CreateUser = new MvxCommand(() => ShowViewModel<CreateUserViewModel>());
            this.userStore = userStore;
            //Login = new MvxCommand<UserStore>(user => ShowViewModel<MessageViewModel>(user));


            Login = new MvxCommand(() =>
            {
            LoginUser();

                
            }); 
            }
        }
    }
