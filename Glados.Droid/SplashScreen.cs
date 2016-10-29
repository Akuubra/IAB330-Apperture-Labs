using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace Glados.Droid
{
    [Activity(
        MainLauncher = true)]
    /*[Activity(
        Label = "Glados.Droid"
        , MainLauncher = true
        , Icon = "@mipmap/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]*/
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
