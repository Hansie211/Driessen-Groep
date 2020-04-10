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
            loginText.Click += (sender, e) => this.SwitchToActivity<MainActivity>(ActivityFlags.ReorderToFront);
        }
    }
}