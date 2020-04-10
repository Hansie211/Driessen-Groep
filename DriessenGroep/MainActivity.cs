using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Xamarin.Essentials;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Widget;
using Android.Content;

namespace DriessenGroep
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            EditText emailText = FindViewById<EditText>(Resource.Id.emailBox);
            emailText.TextChanged += EmailText_TextChanged;

            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
            loginButton.Click += LoginButton_Click;

            TextView registerText = FindViewById<TextView>(Resource.Id.registerText);
            registerText.Click += (sender, e) => this.SwitchToActivity<RegisterActivity>(ActivityFlags.ReorderToFront);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EmailText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Android.Util.Patterns.EmailAddress.Matcher((sender as EditText).Text).Matches())
            {
                Drawable icon = GetDrawable(Resource.Drawable.error);
                icon.SetBounds(0, 0, icon.IntrinsicWidth, icon.IntrinsicHeight);
                (sender as EditText).SetError(string.Format(GetString(Resource.String.invalid_value), GetString(Resource.String.email)), icon);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

