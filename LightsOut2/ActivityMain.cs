using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace LightsOut2
{
    [Activity(Label = "Lights Out!"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Portrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class ActivityMain : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var g = new GameMain();
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

