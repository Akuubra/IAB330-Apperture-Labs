using MvvmCross.Core.ViewModels;
using Glados.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
/// <summary>
/// Author: Jared Bagnall
/// Student Number: n5686245
/// Responsible Screen: User Profile Screen
/// Responsible Files: UserProfileViewModel.cs, UserProfile.axml, UserProfileView.cs
/// </summary>
namespace Glados.Core.ViewModels
{
	public class UserProfileViewModel : MvxViewModel
	{
        private Contact selectedContact;
        public void Init(Contact parameters)
        {
            selectedContact = parameters;
        }
        private string _userName;
        //= "Jared";v

		public string UserName {
			get { return _userName; }
			set {  SetProperty(ref _userName, value); }
		}

        private string _email;
        //= "jaredsbagnall@gmail.com";

		public string Email
		{
			get { return _email; }
			set { SetProperty(ref _email, value); }
		}

        private string _password;
        //= "password";

		public string Password
		{
			get { return _password; }
			set { SetProperty(ref _password, value); }
		}

		//private double _startTime;
		//private double _finishTime;

		// set multiple fields with one button??

		public double SetOfficeHours {

			get; set;
		}
        public override void Start()
        {
            base.Start();
            _userName = selectedContact.FirstName + " " + selectedContact.LastName;
            _email = selectedContact.Email;
        }
    }
}

