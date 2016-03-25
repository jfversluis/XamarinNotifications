using Foundation;
using UIKit;
using WindowsAzure.Messaging;
using XamarinNotifications.Helpers;

namespace XamarinNotifications.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            // registers for push
            var settings = UIUserNotificationSettings.GetSettingsForTypes(
                UIUserNotificationType.Alert
                | UIUserNotificationType.Badge
                | UIUserNotificationType.Sound,
                new NSSet());

            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            // Modify device token for compatibility Azure
            var token = deviceToken.Description;
            token = token.Trim('<', '>').Replace(" ", "");

            // You need the Settings plugin for this!
            Settings.DeviceToken = token;

            // TODO add your own access key
            var hub = new SBNotificationHub("Endpoint=sb://xamarinnotifications-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=<your key here>",
                "xamarinnotifications");

            NSSet tags = null; // create tags if you want
            hub.RegisterNativeAsync(deviceToken, tags, (errorCallback) =>
            {
                if (errorCallback != null)
                {
                    var alert = new UIAlertView("ERROR!", errorCallback.ToString(), null, "OK", null);
                    alert.Show();
                }
            });
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            NSObject inAppMessage;

            var success = userInfo.TryGetValue(new NSString("inAppMessage"), out inAppMessage);

            if (success)
            {
                var alert = new UIAlertView("Notification!", inAppMessage.ToString(), null, "OK", null);
                alert.Show();
            }
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            var alert = new UIAlertView("Computer says no", "Notification registration failed! Try again!", null, "OK", null);

            alert.Show();
        }
    }
}
