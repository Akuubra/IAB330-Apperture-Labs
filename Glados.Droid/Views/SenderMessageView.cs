using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Glados.Droid.Views
{
    [Activity(Label = "View for SenderMessageView", Theme = "@android:style/Theme.Light.NoTitleBar")]
    public class SenderMessageView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SenderMessageView);
        }
    }
}
