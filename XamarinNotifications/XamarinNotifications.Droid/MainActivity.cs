using Android.App;
using Android.Content.PM;
using Android.OS;
using Gcm.Client;
using XamarinNotifications.Droid.PlatformSpecifics;

namespace XamarinNotifications.Droid
{
    [Activity(Label = "XamarinNotifications", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            // Check to ensure everything's setup right for push
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);
            GcmClient.Register(this, NotificationsBroadcastReceiver.SenderIDs);

            LoadApplication(new App());
        }
    }
}