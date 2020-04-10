using Android.App;
using Android.Support.V7.App;

namespace DriessenGroep
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public abstract class BaseActivity : AppCompatActivity
    {
        public override void SetContentView(int layoutResID)
        {
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            base.SetContentView(layoutResID);
        }
    }
}