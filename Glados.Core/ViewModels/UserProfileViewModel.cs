using MvvmCross.Core.ViewModels;
using Glados.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Glados.Core.Interfaces;
using System.Threading.Tasks;
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

        public void Init(Contact contact)
        {
            selectedContact = contact;
            Name = selectedContact.FirstName + " " + selectedContact.LastName;
            Email = selectedContact.Email;
             Location = selectedContact.Location;
            
        }
        private string _name;

		public string Name {
			get { return _name; }
			set {  SetProperty(ref _name, value); }
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


        private string _location;

        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }


    }
}

