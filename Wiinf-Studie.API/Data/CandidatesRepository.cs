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

        private const String SqlSelectPairById = $"SELECT * FROM DoublePaymentPairs WHERE pairId = ";
        private const String SqlSelectAllCandidates = $"SELECT * FROM DoublePaymentCandidates";
        private const String SqlSelectAllPairs = $"SELECT * FROM DoublePaymentPairs";
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

            var result = await _dbConnection.QueryAsync<DoublePaymentPair>(SqlSelectAllPairs);
            var resultMatched = await MatchCandidatesToPair(result);

            return resultMatched;
        }


        //Return one DoublePaymentPair by a given Id
        public async Task<IEnumerable<DoublePaymentPair>> GetPaymentPairById(int pairId){

            var result = await _dbConnection.QueryAsync<DoublePaymentPair>(SqlSelectPairById + pairId);
            var resultMatched = await MatchCandidatesToPair(result);

            return resultMatched;
        }

        // Helpers
        public async Task<IEnumerable<DoublePaymentPair>> MatchCandidatesToPair(IEnumerable<DoublePaymentPair> pairs ){

            var pairData = pairs;
            var rawCandidateData = await _dbConnection.QueryAsync<DoublePaymentCandidate>(SqlSelectAllCandidates);
            foreach (var pair in pairData)
            {
                //Match the candidate objects in the Pair Candidates results  
                pair.Candidate1 = rawCandidateData.ToList().Find(el => el.CandidateId == pair.Candidate1Id);
                pair.Candidate2 = rawCandidateData.ToList().Find(el => el.CandidateId == pair.Candidate2Id);

            }
            return pairData;
        }
        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }
}