namespace MovieSuggest.Interfaces
{
    public interface ICryptoService
    {
        public byte[] HashBytesSha256(string input);
    }
}