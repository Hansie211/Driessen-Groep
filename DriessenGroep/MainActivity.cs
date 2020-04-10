using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Xamarin.Essentials;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Widget;
using Android.Graphics;

namespace DriessenGroep
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            EditText emailText = FindViewById<EditText>(Resource.Id.emailBox);
            emailText.TextChanged += EmailText_TextChanged; ;

            Button confirmButton = FindViewById<Button>(Resource.Id.confirmButton);
            confirmButton.Click += ConfirmButton_Click;
        }

        private void EmailText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Android.Util.Patterns.EmailAddress.Matcher((sender as EditText).Text).Matches())
            {
                Drawable icon = GetDrawable(Resource.Drawable.error);
                icon.SetBounds(0, 0, icon.IntrinsicWidth, icon.IntrinsicHeight);
                (sender as EditText).SetError(GetString(Resource.String.invalid_email), icon);
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            (sender as Button).SetBackgroundColor(Color.Rgb(new Random().Next(256), new Random().Next(256), new Random().Next(256)));
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

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

