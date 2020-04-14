using Android.Content;

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
    }
}