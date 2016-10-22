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
using Microsoft.WindowsAzure.MobileServices;

namespace Application.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        // Define a authenticated user.
      //  private MobileServiceUser user;


        private readonly IUserLogin UserDB;
        private string _userName;
        private UserStore tempUser;
        private LoggedInUser tempUserLoggedIn;
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
                //tempUserLoggedIn = new LoggedInUser();
                //tempUserLoggedIn.UserId = tempUser.Id;
                //tempUserLoggedIn.Email = tempUser.Email;
                //tempUserLoggedIn.Location = tempUser.Location;
                //tempUserLoggedIn.First_Name = tempUser.First_Name;
                //tempUserLoggedIn.Last_Name = tempUser.Last_Name;
                //tempUserLoggedIn.Username = tempUser.Username;
                //tempUserLoggedIn.LoggedIn = true;


                //await UserDB.InsertUser(tempUserLoggedIn);
                ShowViewModel<MessageViewModel>(new { currentUser = tempUser.Id });
            }
                else
            {
                Mvx.Resolve<IToast>().Show("Login Failed!");
            }
            return 1;
        }
        public ICommand CreateUser { get; private set; }
        public LoginViewModel(IUserStoreDatabase userStore, IUserLogin UserDB)
        {
            this.UserDB = UserDB;
            CreateUser = new MvxCommand(() => ShowViewModel<CreateUserViewModel>());
            this.userStore = userStore;


            Login = new MvxCommand(() =>
            {
            LoginUser();

               
            });
        }


        //public async Task<int> Init()
        //{
        //    // loggedInUser = await userStore.GetSingleUser( currentUser);

        //    tempUserLoggedIn = await UserDB.GetSingleUser(true);
        //    if(!(tempUserLoggedIn == null))
        //    {
        //        ShowViewModel<MessageViewModel>(new { currentUser = tempUser.Id });
        //    }

        //    return 1;
        //}
    }
}
