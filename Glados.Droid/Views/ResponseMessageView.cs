using Android.App;
using Android.OS;
using Android.Widget;
using Glados.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace Glados.Droid.Views
{
    [Activity(Label = "View for ResponseMessageView", Theme = "@android:style/Theme.Light.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ResponseMessageView : MvxActivity
    {


        protected ResponseMessageViewModel ResponseMessageViewModel
        {
            get { return ViewModel as ResponseMessageViewModel; }
        }

         CheckBox locationCheck;
        EditText locationEdit;
        TextView locationText;
        CheckBox meetingCheck;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ResponseMessageView);
            locationCheck = (CheckBox)FindViewById(Resource.Id.locationCheck);
            meetingCheck = (CheckBox)FindViewById(Resource.Id.meetingCheck);
            locationEdit = (EditText)FindViewById(Resource.Id.locationEdit);

            locationText = (TextView)FindViewById(Resource.Id.locationText);
            locationEdit.Visibility = Android.Views.ViewStates.Invisible;
            locationText.Visibility = Android.Views.ViewStates.Invisible;
            meetingCheck.Visibility = Android.Views.ViewStates.Invisible;

            if (ResponseMessageViewModel.MeetingRequested)
            {
                meetingCheck.Visibility = Android.Views.ViewStates.Visible;
            }

        }
        protected override void OnStart()
        {
            base.OnStart();

            if (ResponseMessageViewModel.MeetingRequested)
            {
                meetingCheck.Visibility = Android.Views.ViewStates.Visible;
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            ResponseMessageViewModel.PropertyChanged += (s, e) =>
            {
                if (ResponseMessageViewModel.MeetingRequested)
                {
                    meetingCheck.Visibility = Android.Views.ViewStates.Visible;
                }
            };
           
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
