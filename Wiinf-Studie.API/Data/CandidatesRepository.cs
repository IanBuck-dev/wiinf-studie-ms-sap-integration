using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Wiinf_Studie.API.Models;

namespace Wiinf_Studie.API.Data
{
    // Scoped repository that holds all the database access logic.
    public sealed class CandidatesRepository : IDisposable
    {
        private readonly SqliteConnection _dbConnection;

        public CandidatesRepository()
        {
            _dbConnection = new SqliteConnection(DatabaseConfiguration.DatabaseName);
        }

        public async Task<IEnumerable<string>> GetTables()
        {
            var tables = await _dbConnection.QueryAsync<string>("select name from sqlite_schema where type ='table' and name not like 'sqlite_%'");

            return tables;
        }

        public async Task<IEnumerable<DoublePaymentPair>> GetDoublePaymentPairs()
        {
            var result = await _dbConnection.QueryAsync<DoublePaymentPair>("SELECT * FROM DoublePaymentPairs");

            return result;
        }

        // Helpers
        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }
}