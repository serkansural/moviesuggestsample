using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieSuggest.Extensions;
using MovieSuggest.Interfaces;
using MovieSuggest.Models.Response;

namespace MovieSuggest.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _dbService;

        public AccountController(IAccountService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<JsonResult> Register(string username, string password, string email)
        {
            string successMessage = await _dbService.RegisterUser(username, password, email);

            return Json(successMessage.ToSuccessResponseWithoutData());
        }

        [HttpPost]
        public async Task<JsonResult> Login(string username, string password)
        {
            string token = await _dbService.Login(username, password);
            return Json(new LoginResponse()
            {
                Token = token
            });
        }
    }
}