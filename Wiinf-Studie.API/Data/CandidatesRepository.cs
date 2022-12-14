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

        private readonly ILogger<CandidatesRepository> _logger;
        public CandidatesRepository(ILogger<CandidatesRepository> logger)
        {
            _logger = logger;
            _dbConnection = new SqliteConnection(DatabaseConfiguration.DatabaseName);
        }

        /// <summary>
        /// Returns a double payment pair including both candidates.
        /// </summary>
        /// <param name="pairId">The id of the pair to retrieve.</param>
        public async Task<DoublePaymentPair?> GetDoublePaymentPairByIdIncludingCandidates(int pairId)
        {
            var parameters = new { PairId = pairId };
            var sql = @"select *
                from DoublePaymentPairs p
                left join DoublePaymentCandidates c1 on p.Candidate1Id = c1.CandidateId
                left join DoublePaymentCandidates c2 on p.Candidate2Id = c2.CandidateId
                where p.PairId = @PairId";

            var result = await _dbConnection.QueryAsync<DoublePaymentPair, DoublePaymentCandidate, DoublePaymentCandidate, DoublePaymentPair>(sql, (pair, candidate1, candidate2) =>
            {
                pair.Candidate1 = candidate1;
                pair.Candidate2 = candidate2;

                return pair;
            },
            param: parameters,
            splitOn: "CandidateId");

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns all double payment pair including both candidates.
        /// </summary>
        public async Task<IEnumerable<DoublePaymentPair>> GetDoublePaymentPairsIncludingCandidates(RequestContext requestContext)
        {
            // Apply order by
            var orderBy = "ORDER BY ";

            if (!string.IsNullOrEmpty(requestContext.OrderBy))
                orderBy += requestContext.OrderBy;
            else
                orderBy += "PairId asc";

            // Apply filter by
            var filterBy = "";

            if (!string.IsNullOrEmpty(requestContext.FilterBy))
            {
                filterBy += "WHERE ";
                filterBy += requestContext.FilterBy;
            }

            var parameters = new { Page = (requestContext.Page - 1) * requestContext.PageSize, PageSize = requestContext.PageSize };
            var sql = string.Join(" ", @"select *
                from DoublePaymentPairs p
                Left Join DoublePaymentCandidates c1 on p.Candidate1Id = c1.CandidateId
                left join DoublePaymentCandidates c2 on p.Candidate2Id = c2.CandidateId",
                filterBy,
                orderBy,
                "LIMIT @Page, @PageSize");

            // Log statements
            _logger.LogInformation("Executed sql statement: {Statement} with parameters: {Parameters}", sql, parameters);

            var result = await _dbConnection.QueryAsync<DoublePaymentPair, DoublePaymentCandidate, DoublePaymentCandidate, DoublePaymentPair>(sql, (pair, candidate1, candidate2) =>
            {
                pair.Candidate1 = candidate1;
                pair.Candidate2 = candidate2;

                return pair;
            },
            param: parameters,
            splitOn: "CandidateId");

            return result;
        }

        public async Task<DoublePaymentPair?> ChangeJudgementOfDoublePaymentPair(int pairId, string newJudgement)
        {
            var parameters = new { NewJudgement = newJudgement, PairId = pairId };
            var sql = @$"update DoublePaymentPairs
                set Judgement = @NewJudgement
                where PairId = @PairId";

            await _dbConnection.ExecuteAsync(sql, parameters);

            // Return updated pair with candidates.
            return await GetDoublePaymentPairByIdIncludingCandidates(pairId);
        }

        #region Helpers

        public async Task<IEnumerable<string>> GetTables()
        {
            var tables = await _dbConnection.QueryAsync<string>("select name from sqlite_schema where type ='table' and name not like 'sqlite_%'");

            return tables;
        }

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }

        #endregion
    }
}