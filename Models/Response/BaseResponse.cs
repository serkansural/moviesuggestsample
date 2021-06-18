using System.Collections.Generic;

namespace MovieSuggest.Models.Response
{
    public class BaseResponse
    {
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = "";
        public string Message { get; set; } = "";
        public IEnumerable<object> Data { get; set; } = null;
        
        #if DEBUG
        public string StackTrace { get; set; }
        #endif
    }
}