using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MovieSuggest.Interfaces;
using Npgsql;

namespace MovieSuggest.Services
{
    public class DbService : IDbService
    {
        private readonly string _connectionString;

        public DbService(IConfiguration config)
        {
            _connectionString = (string) config.GetValue(typeof(string), "conn_string");
        }

        public async Task<IEnumerable<T>> Query<T>(string query, DynamicParameters parameters)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<T> QuerySingle<T>(string query, DynamicParameters parameters)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}