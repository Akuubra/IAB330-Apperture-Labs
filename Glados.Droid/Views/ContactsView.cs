using Android.App;
using Android.OS;
using Android.Views;
using Android.Content;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Views;
using Glados.Core.Models;


namespace Glados.Droid.Views
{
    [Activity(Label = "View for ContactsViewModel", Theme = "@android:style/Theme.Light.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ContactsView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ContactsView);
            var list = FindViewById<MvxListView>(Resource.Id.ContactsListView);
            list.Adapter = new CustomAdapter(this, (IMvxAndroidBindingContext)BindingContext);
        }

        /// <summary>
        /// The below code was taken from a git repository created by Stuart Lodge. 
        /// Any files in the Collections.Core and Collections.Droid folder that contain polymorphicListItemTypes were referred to
        /// https://github.com/MvvmCross/MvvmCross-Tutorials/blob/master/Working%20With%20Collections/Collections.Droid/Views/PolymorphicListItemTypesView.cs
        /// This helped to add the label for 'favourites' and 'all contacts' to the Contacts listView and give them unique itemLayouts
        /// </summary>
        public class CustomAdapter : MvxAdapter
        {
            public CustomAdapter(Context context, IMvxAndroidBindingContext bindingContext)
                : base(context, bindingContext)
            {
            }

            public override int GetItemViewType(int position)
            {
                var item = GetRawItem(position);
                if (item is ContactLabel)
                    return 0;
                return 1;
            }

            public override int ViewTypeCount
            {
                get { return 2; }
            }

            protected override View GetBindableView(View convertView, object source, int templateId)
            {
                if (source is ContactLabel)
                    templateId = Resource.Layout.ContactsLabelLayout;
                else if (source is ContactWrapper)
                    templateId = Resource.Layout.ContactsLayout;

                return base.GetBindableView(convertView, source, templateId);
            }
        }


    }
}
