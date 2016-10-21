using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace Application.Core.Models
{
    //extends MvxNotifyPropertyChanged only so that it can allow RaisePropertyChanged from the Contact model class
    public class UserStore : MvvmCross.Core.ViewModels.MvxNotifyPropertyChanged
    {
        
        public string Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }




    }
}
