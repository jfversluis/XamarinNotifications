// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XamarinNotifications.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string DeviceTokenKey = "deviceToken_key";
        private static readonly string DeviceTokenDefault = string.Empty;

        #endregion
        
        public static string DeviceToken
        {
            get { return AppSettings.GetValueOrDefault(DeviceTokenKey, DeviceTokenDefault); }
            set { AppSettings.AddOrUpdateValue(DeviceTokenKey, value); }
        }
    }
}