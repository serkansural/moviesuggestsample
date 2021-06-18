using System.Threading.Tasks;
using Dapper;
using MovieSuggest.Interfaces;
using MovieSuggest.Models.MovieDbService;

namespace MovieSuggest.DataAccess
{
    public class MovieRepo : IMovieService
    {
        private readonly IDbService _db;

        public MovieRepo(IDbService db)
        {
            _db = db;
        }

        public async Task<string> SaveMovie(MovieDetailResponse movie)
        {
            var parameters = new DynamicParameters();
            parameters.Add("_id", movie.Id);
            parameters.Add("_name", movie.Name);
            parameters.Add("_origname", movie.OriginalName);
            parameters.Add("_plot", movie.Plot);
            return await _db.QuerySingle<string>("\"movie\".save_movie", parameters);
        }
    }
}