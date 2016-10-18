using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using Application.Core.ViewModels;

namespace Application.Droid.Views
{
    [Activity(Label = "View for FirstViewModel", Theme = "@android:style/Theme.Light.NoTitleBar")]
    public class MessageView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.MessageView);
        }

    }
}
