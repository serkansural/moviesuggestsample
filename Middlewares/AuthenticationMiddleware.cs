using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MovieSuggest.Interfaces;

namespace MovieSuggest.Middlewares
{
    public class AuthenticationMiddleware
    {
        public AuthenticationMiddleware()
        {
        }

        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var securePath = context.Request.Path.StartsWithSegments("/movie");
            if (securePath)
            {
            }
            await _next(context);
        }
    }
}