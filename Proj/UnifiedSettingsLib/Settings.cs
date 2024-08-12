using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharedDataLib
{
    public static class SettingsProvider
    {
        private const String settingsPath = "settings.json";

        public static CurrSettings? TryGet()
        {
            try
            {
                return CurrSettings.FromJson(File.ReadAllText(settingsPath));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <returns>True if write succeeded, false if write failed.</returns>
        public static bool TrySet(CurrSettings newSettings)
        {
            try
            {
                File.WriteAllText(settingsPath, CurrSettings.ToJson(newSettings));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

#pragma warning disable CS8603
    public class CurrSettings
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("language")]
        public SupportedLanguages.SupportedLanguage? Language { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("world_cup_id")]
        public SupportedLanguages.SupportedLanguage? WorldCupID { get; set; }

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
        public List<KeyValuePair<long, String>>? PlayerImagePairs { get; set; }

        public static CurrSettings FromJson(string json) => JsonSerializer.Deserialize<CurrSettings>(json, TooManyUtils.JsonConverters.Settings);
        public static string ToJson(CurrSettings obj) => JsonSerializer.Serialize(obj);
    }
#pragma warning restore CS8603
}
