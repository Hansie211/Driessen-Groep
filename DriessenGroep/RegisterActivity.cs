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

            Button confirmButton = FindViewById<Button>( Resource.Id.confirmButton );
            confirmButton.Click += ConfirmButton_Click;
        }

        private async void ConfirmButton_Click( object sender, System.EventArgs e ) {


            string firstName = FindViewById<EditText>( Resource.Id.firstName )?.Text;
            string affix = FindViewById<EditText>( Resource.Id.affix )?.Text;
            string lastName = FindViewById<EditText>( Resource.Id.lastName )?.Text;
            string emailAddress = FindViewById<EditText>( Resource.Id.emailAddress )?.Text;
            string password = FindViewById<EditText>( Resource.Id.password )?.Text;

            if ( !string.IsNullOrEmpty(affix) ) {

                lastName = string.Format("{0} {1}", affix, lastName );
            }

            APIResponse<string> response = await API.CreateUserAsync( firstName, lastName, emailAddress, password );

            if ( response.IsSuccess ) {

                // Store the token
                API.SetAccessToken( response.Content );

                // Switch to events
                ( this ).SwitchToActivity<MainActivity>( ActivityFlags.ReorderToFront );
                return;
            }

            // Display error message

            System.Console.WriteLine( response );
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