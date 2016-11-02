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
        LinearLayout completeButton;
        LinearLayout nudgeCancelButton;
        LinearLayout nudgeCancelButton2;
        Button cancel;
        Button nudge;
        Button complete;


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
            completeButton = (LinearLayout)FindViewById(Resource.Id.completeButton);
            completeButton.Visibility = Android.Views.ViewStates.Gone;
            nudgeCancelButton = (LinearLayout)FindViewById(Resource.Id.nudgeCancelButton);
            nudgeCancelButton.Visibility = Android.Views.ViewStates.Gone;
            nudgeCancelButton2 = (LinearLayout)FindViewById(Resource.Id.nudgeCancelButton2);
            nudgeCancelButton2.Visibility = Android.Views.ViewStates.Gone;

            cancel = (Button)FindViewById(Resource.Id.CancelBTN);
            cancel.Visibility = Android.Views.ViewStates.Gone;
            nudge = (Button)FindViewById(Resource.Id.NudgeBTN);
            nudge.Visibility = Android.Views.ViewStates.Gone;
            complete = (Button)FindViewById(Resource.Id.CompleteBTN);
            complete.Visibility = Android.Views.ViewStates.Gone;

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
                    completeButton.Visibility = Android.Views.ViewStates.Visible;
                    complete.Visibility = Android.Views.ViewStates.Visible;

                    nudgeCancelButton.Visibility = Android.Views.ViewStates.Gone;
                    nudgeCancelButton2.Visibility = Android.Views.ViewStates.Gone;
                    cancel.Visibility = Android.Views.ViewStates.Gone;
                    nudge.Visibility = Android.Views.ViewStates.Gone;
                }
                else
                {
                    responseLayout.Visibility = Android.Views.ViewStates.Gone;
                    completeButton.Visibility = Android.Views.ViewStates.Gone;
                    complete.Visibility = Android.Views.ViewStates.Gone;

                    nudgeCancelButton.Visibility = Android.Views.ViewStates.Visible;
                    nudgeCancelButton2.Visibility = Android.Views.ViewStates.Visible;
                    cancel.Visibility = Android.Views.ViewStates.Visible;
                    nudge.Visibility = Android.Views.ViewStates.Visible;
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
