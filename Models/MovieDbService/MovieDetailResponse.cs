using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MovieSuggest.Models.MovieDbService
{
    public class MovieDetailResponse : BaseResponse
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("title")] public string Name { get; set; }
        [JsonPropertyName("original_title")] public string OriginalName { get; set; }
        [JsonPropertyName("overview")] public string Plot { get; set; }
        [JsonPropertyName("genres")] public IEnumerable<Genre> Genres { get; set; }
    }
}