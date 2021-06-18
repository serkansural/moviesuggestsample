using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace MovieSuggest.Interfaces
{
    public interface IDbService
    {
        public Task<IEnumerable<T>> Query<T>(string query, DynamicParameters parameters);
        public Task<T> QuerySingle<T>(string query, DynamicParameters parameters);
    }
}