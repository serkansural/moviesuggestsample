using System;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieSuggest.DataAccess;
using MovieSuggest.Interfaces;
using MovieSuggest.Middlewares;
using MovieSuggest.Models.Response;
using MovieSuggest.Services;

namespace MovieSuggest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICryptoService, CryptoService>();
            services.AddSingleton<IDbService, DbService>();
            services.AddSingleton<IAccountService, AccountRepo>();
            services.AddSingleton<IMovieService, MovieRepo>();
            services.AddHostedService<MovieDbService>();
            services.AddMvcCore(
                options => { options.EnableEndpointRouting = false; });
        }

        public void Configure(IConfiguration configuration, IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(c =>
            {
                c.Run(async (context) =>
                {
                    var exception = context.Features.Get<IExceptionHandlerPathFeature>();
                    await context.Response.WriteAsJsonAsync(new BaseResponse
                    {
                        IsError = true,
                        ErrorMessage = exception.Error.Message,
#if DEBUG
                        StackTrace = exception.Error.StackTrace
#endif
                    });
                });
            });
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseRouting();
            app.UseMvc(
                (builder) =>
                {
                    builder.MapRoute("moviesuggest",
                        "api/{controller}/{action}"
                    );
                }
            );

            app.Run(async (context) => { await context.Response.WriteAsync("MovieSuggest API V0.1"); });
        }
    }
}