using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Database
{
    public class DbContext
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public DbContext(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> Query<T>(string query, object param = null)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    return await con.QueryAsync<T>(query, param);
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Failed to Query<T>", query, param);
                throw;
            }
        }

        public async Task<int> Execute(string query, object param = null)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    return await con.ExecuteAsync(query, param);
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Failed to Execute", query, param);
                throw;
            }
        }

        public Transaction NewTransaction()
        {
            return new Transaction(_connectionString, _logger);
        }
    }
}
