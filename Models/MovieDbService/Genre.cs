using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MovieSuggest.Models.MovieDbService
{
    public class Genre
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
    }
}