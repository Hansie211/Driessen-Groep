using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;

namespace DriessenGroep
{
    internal static class ActivityHelpers
    {
        internal static void SwitchToActivity<T>(this ContextWrapper contextWrapper, ActivityFlags flags) where T : BaseActivity
        {
            Intent intent = new Intent(contextWrapper, typeof(T));
            intent.SetFlags(flags);
            contextWrapper.StartActivity(intent);
        }

        internal static void DisplayTextError(this Context context, object sender, string error)
        {
            Drawable icon = context.GetDrawable(Resource.Drawable.error);
            icon.SetBounds(0, 0, icon.IntrinsicWidth, icon.IntrinsicHeight);
            (sender as TextView).SetError(error, icon);
        }
    }
}