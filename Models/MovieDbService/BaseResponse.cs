namespace MovieSuggest.Models.MovieDbService
{
    public class BaseResponse
    {
        public bool success { get; set; } = true;
        public int status_code { get; set; }
        public string status_message { get; set; }
    }
}