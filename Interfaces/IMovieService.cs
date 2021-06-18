using System.Threading.Tasks;
using MovieSuggest.Models.MovieDbService;

namespace MovieSuggest.Interfaces
{
    public interface IMovieService
    {
        public Task<string> SaveMovie(MovieDetailResponse movie);
    }
}