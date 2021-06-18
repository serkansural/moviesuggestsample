using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MovieSuggest.Interfaces;
using MovieSuggest.Models.MovieDbService;

namespace MovieSuggest.Services
{
    public class MovieDbService : IHostedService
    {
        private int movieCounter = 2;
        private readonly string _apiKey;
        private readonly string _baseEndpoint;
        private readonly int intervalInSeconds;
        private readonly IMovieService _movieService;
        private Timer _timer;

        public MovieDbService(IConfiguration config, IMovieService service)
        {
            _movieService = service;
            _apiKey = (string) config.GetValue(typeof(string), "moviedb_api_key");
            _baseEndpoint = (string) config.GetValue(typeof(string), "moviedb_base_endpoint");
            intervalInSeconds = (int) config.GetValue(typeof(int), "moviedb_crawl_interval_seconds");
        }

        private async Task<MovieDetailResponse> GetMovie(int id)
        {
            using var client = new HttpClient();
            var content = new StringContent($"api_key=${_apiKey}");
            string requestUri = $"{_baseEndpoint}movie/{id}?api_key={_apiKey}";
            var response =
                await client.GetAsync(requestUri, CancellationToken.None);
            var responseAsStr = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MovieDetailResponse>(responseAsStr);
        }

        private void TryNewMovie(object state)
        {
            try
            {
                var movieDetail = GetMovie(movieCounter).Result;
                Interlocked.Increment(ref movieCounter);
                if (movieDetail.success)
                {
                    _movieService.SaveMovie(movieDetail).Wait();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(TryNewMovie, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(intervalInSeconds));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}