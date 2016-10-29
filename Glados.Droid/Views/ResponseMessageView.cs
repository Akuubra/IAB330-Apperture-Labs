using Android.App;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Views;

namespace Glados.Droid.Views
{
    [Activity(Label = "View for ResponseMessageView", Theme = "@android:style/Theme.Light.NoTitleBar")]
    public class ResponseMessageView : MvxActivity
    {

        CheckBox locationCheck;
        EditText locationEdit;
        TextView locationText;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ResponseMessageView);
            locationCheck = (CheckBox)FindViewById(Resource.Id.locationCheck);
            locationEdit = (EditText)FindViewById(Resource.Id.locationEdit);

            locationText = (TextView)FindViewById(Resource.Id.locationText);
            locationEdit.Visibility = Android.Views.ViewStates.Invisible;
            locationText.Visibility = Android.Views.ViewStates.Invisible;
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (locationCheck != null)
            {
                locationCheck.CheckedChange += (s, e) =>
                {
                        if (locationCheck.Checked)
                        {
                        locationEdit.Visibility = Android.Views.ViewStates.Visible;
                        locationText.Visibility = Android.Views.ViewStates.Visible;
                        }
                        else
                        {
                        locationEdit.Visibility = Android.Views.ViewStates.Invisible;
                        locationText.Visibility = Android.Views.ViewStates.Invisible;

                        }
                    
                };

            }
        }
    }
}
