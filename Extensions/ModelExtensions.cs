using MovieSuggest.Models.Response;

namespace MovieSuggest.Extensions
{
    public static class ModelExtensions
    {
        public static BaseResponse ToSuccessResponseWithoutData(this string message)
        {
            return new BaseResponse()
            {
                Message = message
            };
        }
    }
}