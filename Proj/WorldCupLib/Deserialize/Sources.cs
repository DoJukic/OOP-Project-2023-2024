// To parse this JSON data, add NuGet 'System.Text.Json' then do:
//
//    using WorldCupLib.Deserialize;
//
//    var sources = RemoteDataSource.FromJson(jsonString);
#nullable enable
#pragma warning disable CS8618
#pragma warning disable CS8601
#pragma warning disable CS8603

namespace WorldCupLib.Deserialize
{
    using System;
    using System.Collections.Generic;

    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Globalization;

    public partial class RemoteDataSource
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("internalImageID")]
        public string InternalImageID { get; set; }
    }

    public partial class RemoteDataSource
    {
        public static List<RemoteDataSource> FromJson(string json) => JsonSerializer.Deserialize<List<RemoteDataSource>>(json, TooManyUtils.JsonConverters.Settings);
    }
}
#pragma warning restore CS8618
#pragma warning restore CS8601
#pragma warning restore CS8603
