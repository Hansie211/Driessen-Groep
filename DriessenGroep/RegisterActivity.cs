using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace DriessenGroep
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class RegisterActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register_main);

            TextView loginText = FindViewById<TextView>(Resource.Id.loginText);
            loginText.Click += (sender, e) => (this).SwitchToActivity<MainActivity>(ActivityFlags.ReorderToFront);

            EditText emailText = FindViewById<EditText>(Resource.Id.emailAddress);
            emailText.FocusChange += EmailText_FocusChange;
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
    }
}