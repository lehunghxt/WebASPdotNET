namespace Web.Asp.Provider
{
    /// <summary>
    /// Class that managed application settings from every sources.
    /// </summary>
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