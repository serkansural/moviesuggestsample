using System.Threading.Tasks;
using Dapper;
using MovieSuggest.Interfaces;
using MovieSuggest.Models.Response;
using MovieSuggest.Services;

namespace MovieSuggest.DataAccess
{
    public class AccountRepo : IAccountService
    {
        private readonly ICryptoService _crypto;
        private readonly IDbService _db;

        public AccountRepo(ICryptoService crypto, IDbService db)
        {
            _db = db;
            _crypto = crypto;
        }

        public async Task<string> RegisterUser(string username, string pass, string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("_username", username);
            var passwordHash = _crypto.HashBytesSha256(pass);
            parameters.Add("_password", passwordHash);
            parameters.Add("_email", email);
            return await _db.QuerySingle<string>("\"user\".register_user", parameters);
        }

        public async Task<string> Login(string username, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("_username", username);
            parameters.Add("_password", _crypto.HashBytesSha256(password));
            return await _db.QuerySingle<string>("\"user\".login", parameters);
        }
    }
}