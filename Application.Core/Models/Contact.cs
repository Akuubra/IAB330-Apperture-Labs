using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Core.Models
{
    public class Contact : MvvmCross.Core.ViewModels.MvxNotifyPropertyChanged
    {
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        private bool _isFavourite;
        public bool IsFavourite {
            get { return _isFavourite; }
            set { _isFavourite = value;
                RaisePropertyChanged(() => IsFavourite);
            }
        }
        public string ImagePath { get; set; }



        public Contact() { }
        public Contact(string contactFirstName, string contactLastName, string contactEmail, bool isFav)
        {
            _isFavourite = isFav;
            ContactFirstName = contactFirstName;
            ContactLastName = contactLastName;
            ContactEmail = contactEmail;
        }
    }
}
