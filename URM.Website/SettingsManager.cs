namespace URM.Website
{
    /// <sURMmary>
    /// Class that managed application settings from every sources.
    /// </sURMmary>
    public static class SettingsManager
    {
        public static AppSettings AppSettings { get; private set; }
        public static Constant Constants { get; private set; }

        static SettingsManager()
        {
            AppSettings = new AppSettings();
            Constants = new Constant();
        }
    }
}