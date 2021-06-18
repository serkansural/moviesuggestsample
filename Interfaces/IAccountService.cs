using System.Threading.Tasks;

namespace MovieSuggest.Interfaces
{
    public interface IAccountService
    {
        public Task<string> RegisterUser(string username, string password, string email);
        public Task<string> Login(string username, string password);
    }
}