using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Converters;
using Android.Graphics;

namespace Glados.Droid.ValueConverters
{
    public class SendImageValueConverter : MvxValueConverter<bool, int>
    {
        protected override int Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value)
            {
                return Resource.Drawable.forward_blue;
            }
            else
            {
                return Resource.Drawable.back_orange;
            }
        }
    }
}