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
        private const string DatabaseNameKey = "DatabaseName";
        private readonly string _databaseName;
        private readonly SqliteConnection _dbConnection;

        public CandidatesRepository(IConfiguration config)
        {
            _databaseName = config.GetValue(DatabaseNameKey, "Data Source=Product.sqlite");
            _dbConnection = new SqliteConnection(_databaseName);
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