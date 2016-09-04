using MvvmCross.Core.ViewModels;
using Application.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Application.Core.ViewModels
{
	public class UserViewModel : MvxViewModel
	{

		private string _userName = "Jared";

		public string UserName {
			get { return _userName; }
		}

		private string _email = "jaredsbagnall@gmail.com";

		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}

		private string _password = "password";

		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		//private double _startTime;
		//private double _finishTime;

		// set multiple fields with one button??

		public double SetOfficeHours {

			get; set;
		}
	}
}

