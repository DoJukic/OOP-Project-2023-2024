using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using static SharedDataLib.SupportedLanguages;

namespace SharedDataLib
{
    public static class SettingsProvider
    {
        private const String settingsPath = "settings.json";
        private static FileStream? lockStream;

        private static bool error = false;
        private static CurrSettings settings;


        static SettingsProvider()
        {
            try
            {
                settings = CurrSettings.FromJson(File.ReadAllText(settingsPath));
            }
            catch (Exception)
            {
                if (File.Exists(settingsPath))
                {
                    error = true;
                }
                settings = new();
            }
            TryLock();
        }

        public static CurrSettings GetSettings()
        {
            return settings;
        }
        public static bool GetErrorOccured()
        {
            return error;
        }

        private static bool TryLock()
        {
            try
            {
                lockStream = File.Open(settingsPath, FileMode.Append, FileAccess.Write, FileShare.None);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static void Unlock()
        {
            if (lockStream != null)
            {
                lockStream.Dispose();
            }
        }

        /// <returns>True if write succeeded, false if write failed.</returns>
        public static bool TrySave()
        {
            if (error)
                return false;

            Unlock();
            try
            {
                File.WriteAllText(settingsPath, CurrSettings.ToJson(settings));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                TryLock();
            }
        }

        public static WorldCupData? TryGetDataFromGuid(CurrSettings settings, String guid)
        {
            if (settings.WorldCupDataList == null)
                return null;

            foreach (var cupData in settings.WorldCupDataList)
            {
                if (cupData.GUID == guid)
                    return cupData;
            }

            return null;
        }

#pragma warning disable CS8603
        public class CurrSettings
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("language")]
            public SupportedLanguage? Language { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("selected_world_cup_guid")]
            public String? SelectedWorldCupGUID { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("world_cup_data")]
            public List<WorldCupData>? WorldCupDataList { get; set; }

            public static CurrSettings FromJson(string json) => JsonSerializer.Deserialize<CurrSettings>(json, TooManyUtils.JsonConverters.Settings);
            public static string ToJson(CurrSettings obj) => JsonSerializer.Serialize(obj);
        }

        public class WorldCupData
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("guid")]
            public String? GUID { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("selected_team_fifa_ID")]
            public String? SelectedTeamFifaID { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("teams")]
            public List<TeamData>? TeamDataList { get; set; }
        }

        public class TeamData
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("fifa_ID")]
            public String? FifaID { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("fav_player_1_shirt_num")]
            public long? FavPlayer1ShirtNum { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("fav_player_2_shirt_num")]
            public long? FavPlayer2ShirtNum { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("fav_player_3_shirt_num")]
            public long? FavPlayer3ShirtNum { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonPropertyName("player_image_pairs")]
            public List<KeyValuePair<long, String>>? PlayerImagePairList { get; set; }
        }
    }
#pragma warning restore CS8603
}
