using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Application.Droid.Views
{
    [Activity(Label = "View for ContactsViewModel")]
    public class ContactsView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ContactsView);
        }
    }
}
