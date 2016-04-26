using Microsoft.Azure.NotificationHubs;

namespace XamarinNotifications.Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Define the notification hub.
            // TODO Add your own connectionstring and hub name here
            var hub =
                NotificationHubClient.CreateClientFromConnectionString(
                    "<your connectionstring here>", "<your hub name here>");

            // Define an iOS alert..
            var iOSalert =
                        "{\"aps\":{\"alert\":\"Hello. This is a iOS notification! Tada!\", \"sound\":\"default\"}}";

            // ..And send it
            hub.SendAppleNativeNotificationAsync(iOSalert).Wait();

            // Define an Anroid alert..
            var androidAlert = "{\"alert\": \"Can I interest you in a once in a lifetime push notification?!\", \"title\":\"Ding, dong!\"}";

            // ..And send it
            hub.SendGcmNativeNotificationAsync(androidAlert).Wait();

            // Define an Windows Phone alert..
            var winPhoneAlert = "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
                + "<toast>"
                + "<visual><binding template=\"ToastText01\">"
                + "<text id=\"1\">Peek-a-book</text>"
                + "<text id=\"2\">This is your friendly neighborhood message!</text>"
                + "</binding>"
                + "</visual>"
                + "</toast>";

            // ..And send it
            hub.SendWindowsNativeNotificationAsync(winPhoneAlert).Wait();
        }
    }
}