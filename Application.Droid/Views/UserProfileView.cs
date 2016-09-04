using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Application.Droid.Views
{
    [Activity(Label = "User Profile")]
    public class UserProfileView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.UserProfile);
        }
    }
}
