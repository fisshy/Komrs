using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Komrs.Product.Infrastructure
{
    public class DbContext
    {
        private readonly string connectionString;

        public DbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> Query<T>(string query, object param = null)
        {
            try
            {
                using(var con = new SqlConnection())
                {
                    return await con.QueryAsync<T>(query, param);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
