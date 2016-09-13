using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Application.Droid.Views
{
    [Activity(Label = "View for FirstViewModel", Theme = "@android:style/Theme.Light.NoTitleBar")]
    public class FirstView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.FirstView);
        }
    }
}
