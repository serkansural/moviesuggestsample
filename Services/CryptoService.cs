using System.Security.Cryptography;
using System.Text;
using MovieSuggest.Interfaces;

namespace MovieSuggest.Services
{
    public class CryptoService : ICryptoService
    {
        public byte[] HashBytesSha256(string input)
        {
            using var crypto = new SHA256CryptoServiceProvider();
            return crypto.ComputeHash(Encoding.UTF8.GetBytes(input));
        }
    }
}