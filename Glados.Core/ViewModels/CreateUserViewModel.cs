using MvvmCross.Core.ViewModels;
using Glados.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Glados.Core.Interfaces;
using System.Diagnostics;
using MvvmCross.Platform;

/// <summary>
/// Author: Jared Bagnall
/// Student Number: n5686245
/// Responsible Screen: User Profile Screen
/// Responsible Files: UserProfileViewModel.cs, UserProfile.axml, UserProfileView.cs
/// </summary>
namespace Glados.Core.ViewModels
{
	public class CreateUserViewModel : MvxViewModel
	{
        IUserStoreDatabase userStore;

        UserStore userTemp;


        private string _userName;

		public string UserName {
			get { return _userName; }
			set {  SetProperty(ref _userName, value); }
		}

        private string _email;

		public string Email
		{
			get { return _email; }
			set { SetProperty(ref _email, value); }
		}

        private string _password;

		public string Password
		{
			get { return _password; }
			set { SetProperty(ref _password, value); }
		}

        private string _first_namme;

        public string First_Name
        {
            get { return _first_namme; }
            set { SetProperty(ref _first_namme, value); }
        }

        private string _last_name;

        public string Last_Name
        {
            get { return _last_name; }
            set { SetProperty(ref _last_name, value); }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }
        private string _id;

        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public ICommand AddUser { get; private set; }

        public CreateUserViewModel(IUserStoreDatabase userStore)
        {
            this.userStore = userStore;
          //  SelectContactCommandToast = new MvxCommand(SelectContactToast);
            //()=> Mvx.Resolve<IToast>().Show("Message Sent!")

            AddUser = new MvxCommand(()=>{
                addNewUser();
                ShowViewModel<LoginViewModel>();
                Mvx.Resolve<IToast>().Show("User Created");
            });
        }

        private async void addNewUser()
        {
            user();
            //Debug.WriteLine(userTemp.First_Name.GetType());
            //Debug.WriteLine(userTemp.Last_Name.GetType());
            //Debug.WriteLine(userTemp.Username.GetType());
            //Debug.WriteLine(userTemp.Location.GetType());
            //Debug.WriteLine(userTemp.Email.GetType());
            await userStore.InsertUser(userTemp);
        }

        private void user()
        {
            userTemp = new UserStore();
            //userTemp.Id = 1;
            userTemp.Email = (string)this.Email;
            userTemp.Username = (string)this.UserName;
            userTemp.First_Name = (string)this.First_Name;
            userTemp.Last_Name = (string)this.Last_Name;
            userTemp.Location = (string)this.Location;
            userTemp.Password = (string)this.Password;
          
        }

       
    }
}

