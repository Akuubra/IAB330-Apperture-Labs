using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Application.Droid.Views
{
    [Activity(Label = "View for ContactsViewModel", Theme = "@android:style/Theme.Light.NoTitleBar")]
    public class CreateUserView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CreateUserView);
        }
    }
}
