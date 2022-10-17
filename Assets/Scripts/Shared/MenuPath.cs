namespace AdventureGame.Shared
{

    public static class MenuPath
    {
        // Base
        public const string ScriptableObjects = "Scriptable Objects/";

        // Cathegories
        public const string Inputs = ScriptableObjects + "Inputs/";
        public const string Settings = ScriptableObjects + "Settings/";

        public const string SettingsSub = Settings + "Sub/";
        public const string SettingsPlayers = SettingsSub + "Players/";
        public const string SettingsGlobal = Settings + "Global/";
        public const string SettingsEnemies = SettingsSub + "Enemies/";
    }
}