using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Transaction : IDisposable
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        private SqlConnection _con;
        private SqlTransaction _trans;

        public Transaction(string connectionString, ILogger logger)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException("Connection not provided");
            _logger = logger;
        }

        public void Begin()
        {
            Dispose();
            _con = new SqlConnection(_connectionString);
            _con.Open();
            _trans = _con.BeginTransaction();
        }

        public async Task<int> Execute(string query, object param = null)
        {
            try
            {
                if (_trans == null || _con == null)
                {
                    throw new ArgumentException("Transaction is not started, call Begin() to start");
                }
                await _con.OpenAsync();
                return await _con.ExecuteAsync(query, param);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Failed to Execute", query, param);
                throw;
            }
        }

        public void Commit()
        {
            if (_trans != null)
            {
                _trans.Commit();
            }
        }

        public void Rollback()
        {
            if (_trans != null)
            {
                _trans.Rollback();
            }
        }

        public void Dispose()
        {
            if (_trans != null)
            {
                _trans.Dispose();
                _trans = null;
            }

            if (_con != null)
            {
                _con.Close();
                _con.Dispose();
                _con = null;
            }

        }
    }
}
