using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace DriessenGroep
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_main);

            EditText emailText = FindViewById<EditText>(Resource.Id.emailBox);
            emailText.FocusChange += EmailText_FocusChange;
            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
            loginButton.Click += LoginButton_Click;

            TextView registerText = FindViewById<TextView>(Resource.Id.registerText);
            registerText.Click += (sender, e) => (this).SwitchToActivity<RegisterActivity>(ActivityFlags.ReorderToFront);
        }

        private void EmailText_FocusChange(object sender, Android.Views.View.FocusChangeEventArgs e)
        {
            if (!e.HasFocus)
            {
                if (!Android.Util.Patterns.EmailAddress.Matcher((sender as EditText).Text).Matches())
                {
                    this.DisplayTextError(sender, string.Format(GetString(Resource.String.invalid_value), GetString(Resource.String.email)));
                }
            }
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {

            string emailAddress = FindViewById<EditText>( Resource.Id.emailBox )?.Text;
            string password = FindViewById<EditText>( Resource.Id.passwordBox )?.Text;

            APIResponse<string> response = await API.LoginUserAsync( emailAddress, password );

            if ( response.IsSuccess ) {
                API.SetAccessToken( response.Content );

                // Switch to events
                ( this ).SwitchToActivity<MainActivity>( ActivityFlags.ReorderToFront );
                return;
            }

            // Display error
        }
    }
}

