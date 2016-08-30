using MvvmCross.Core.ViewModels;

namespace Application.Core.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {
        private string _hello = "Hello";
        public string Hello
        { 
            get { return _hello; }
            set { SetProperty (ref _hello, value); }
        }
        private string _click = "Click me!";
        public string Click
        {
            get { return _click; }
            set { SetProperty(ref _click, value); }
        }
    }
}
