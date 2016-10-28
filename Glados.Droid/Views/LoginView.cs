using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Java.Lang;
using Glados.Droid.Database;
using MvvmCross.Core.ViewModels;
using Glados.Core.ViewModels;
using Glados.Core.Interfaces;

namespace Glados.Droid.Views
{
    [Activity(Label = "View for FirstViewModel", Theme = "@android:style/Theme.Light.NoTitleBar")]
    public class LoginView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginLayout);
        }


       
    }
}