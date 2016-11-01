using Android.App;
using Android.OS;
using Android.Widget;
using Glados.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace Glados.Droid.Views
{
    [Activity(Label = "View for SenderMessageView", Theme = "@android:style/Theme.Light.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SenderMessageView : MvxActivity
    {


        protected SenderMessageViewModel SenderMessageViewModel
        {
            get { return ViewModel as SenderMessageViewModel; }
        }


        CheckBox meetingCheck;
        EditText timeEdit;
        TextView timeText;
        EditText meetLocEdit;
        TextView meetLocText;
        LinearLayout responseLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SenderMessageView);
            meetingCheck = (CheckBox)FindViewById(Resource.Id.meetingCheck);
            timeEdit = (EditText)FindViewById(Resource.Id.timeField);

            timeText = (TextView)FindViewById(Resource.Id.tmeTag);
            meetLocEdit = (EditText)FindViewById(Resource.Id.meetLocField);

            meetLocText = (TextView)FindViewById(Resource.Id.meetLocTag);

            responseLayout = (LinearLayout)FindViewById(Resource.Id.responseLayout);
            responseLayout.Visibility = Android.Views.ViewStates.Gone;

            if (meetingCheck.Checked)
            {
                timeEdit.Visibility = Android.Views.ViewStates.Visible;
                timeText.Visibility = Android.Views.ViewStates.Visible;
                meetLocEdit.Visibility = Android.Views.ViewStates.Visible;
                meetLocText.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                timeEdit.Visibility = Android.Views.ViewStates.Invisible;
                timeText.Visibility = Android.Views.ViewStates.Invisible;
                meetLocEdit.Visibility = Android.Views.ViewStates.Invisible;
                meetLocText.Visibility = Android.Views.ViewStates.Invisible;

            }
        }

        protected override void OnResume()
        {

            SenderMessageViewModel.PropertyChanged += (s, e) =>
            {
                if (SenderMessageViewModel.ResponseReceived)
                {
                    responseLayout.Visibility = Android.Views.ViewStates.Visible;
                }
            };

            base.OnResume();
            if (meetingCheck != null)
            {
                meetingCheck.CheckedChange += (s, e) =>
                {
                        if (meetingCheck.Checked)
                        {
                            timeEdit.Visibility = Android.Views.ViewStates.Visible;
                            timeText.Visibility = Android.Views.ViewStates.Visible;
                        meetLocEdit.Visibility = Android.Views.ViewStates.Visible;
                        meetLocText.Visibility = Android.Views.ViewStates.Visible;
                    }
                        else
                        {
                            timeEdit.Visibility = Android.Views.ViewStates.Invisible;
                            timeText.Visibility = Android.Views.ViewStates.Invisible;
                        meetLocEdit.Visibility = Android.Views.ViewStates.Invisible;
                        meetLocText.Visibility = Android.Views.ViewStates.Invisible;


                    }

                };

            }
        }
    }
}
