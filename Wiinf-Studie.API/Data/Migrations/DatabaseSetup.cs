using System.Globalization;
using Bogus;
using Dapper;
using Microsoft.Data.Sqlite;
using Wiinf_Studie.API.Models;
using Wiinf_Studie.API.Utils;

namespace Wiinf_Studie.API.Data.Migrations;

public static class DatabaseSetup
{
    public static async Task SeedDatabaseWithDummyData(int numberOfPairs = 100)
    {
        using var dbConnection = new SqliteConnection(DatabaseConfiguration.DatabaseName);

        // Create fake objects.
        var candidatesIds = 1;
        var candidatesFaker = new Faker<DoublePaymentCandidate>()
            .RuleFor(p => p.CandidateId, f => candidatesIds++)
            .RuleFor(p => p.DocumentId, f => Guid.NewGuid().ToString())
            .RuleFor(p => p.Amount, f => f.Finance.Amount(10, 10000))
            .RuleFor(p => p.Currency, f => f.PickRandom(Currencies.Allowed))
            .RuleFor(p => p.DocumentType, f => f.PickRandom(DocumentTypes.Allowed))
            .RuleFor(p => p.CompanyCode, f => f.Commerce.Ean8())
            .RuleFor(p => p.VendorNumber, f => f.Commerce.Ean8())
            .RuleFor(p => p.SAPClient, f => f.Commerce.Ean13())
            .RuleFor(p => p.FiscalYear, f => f.Random.Int(2020, 2022))
            .RuleFor(p => p.PurchasingDocumentId, f => Guid.NewGuid().ToString())
            .RuleFor(p => p.TransactionCode, f => Guid.NewGuid().ToString())
            .RuleFor(p => p.AccountingDate, f => f.Date.Between(new DateTime(2020, 1, 1), new DateTime(2022, 12, 31)))
            .RuleFor(p => p.DocumentDate, f => f.Date.Between(new DateTime(2020, 1, 1), new DateTime(2022, 12, 31)))
            .RuleFor(p => p.ClearingDate, f => f.Date.Between(new DateTime(2020, 1, 1), new DateTime(2022, 12, 31)))
            .RuleFor(p => p.Username, f => f.Internet.UserName())
            .RuleFor(p => p.ClearingDocumentId, f => Guid.NewGuid().ToString());

        var pairIds = 1;
        // Only use each candidate once for now.
        var candidatesForeignKeys = 1;
        var pairsFaker = new Faker<DoublePaymentPair>()
            .RuleFor(p => p.PairId, f => pairIds++)
            .RuleFor(p => p.Candidate1Id, f => candidatesForeignKeys++)
            .RuleFor(p => p.Candidate2Id, f => candidatesForeignKeys++)
            .RuleFor(p => p.Judgement, f => f.PickRandom(Judgments.Allowed))
            .RuleFor(p => p.Score, f => f.Random.Decimal(1.0m, 15.0m));

        // Clean up
        // in case of already existing data, you need to set the sequences of the auto incrementing ids to NULL in SQLite.
        var cleanUpSql = "update sqlite_sequence set seq = NULL where name in ('DoublePaymentPairs', 'DoublePaymentCandidates')";
        var affectedRows = await dbConnection.ExecuteAsync(cleanUpSql);
        Console.WriteLine($"Cleaned up {affectedRows} rows from sequences table.");

        // Every pair has 2 candidates.
        var candidates = candidatesFaker.Generate(numberOfPairs * 2);
        var pairs = pairsFaker.Generate(numberOfPairs);

        // Insert candidates first
        var candidatesInsertValues = new List<string>();

        foreach (var c in candidates)
        {
            candidatesInsertValues.Add($"('{c.DocumentId}', '{c.Amount.ToString(CultureInfo.InvariantCulture)}', '{c.Currency}', '{c.DocumentType}', '{c.CompanyCode}', '{c.VendorNumber}', '{c.SAPClient}', {c.FiscalYear}, '{c.PurchasingDocumentId}', '{c.TransactionCode}', '{c.AccountingDate.ToISO8601Format()}', '{c.DocumentDate.ToISO8601Format()}', '{c.Username}', '{c.ClearingDocumentId}', '{c.ClearingDate.ToISO8601Format()}')");
        }

        var candidatesInsertSQL =
        @$"insert into DoublePaymentCandidates (DocumentId, Amount, Currency, DocumentType, CompanyCode, VendorNumber, SAPClient, FiscalYear, PurchasingDocumentId, TransactionCode, AccountingDate, DocumentDate, Username, ClearingDocumentId, ClearingDate)
            values {string.Join(" ,", candidatesInsertValues)};";

        affectedRows = await dbConnection.ExecuteAsync(candidatesInsertSQL);
        Console.WriteLine($"Affected rows for candidates insert: {affectedRows}");

        var pairsInsertValues = new List<string>();

        foreach (var p in pairs)
        {
            pairsInsertValues.Add($"('{p.Judgement}', '{p.Score.ToString(CultureInfo.InvariantCulture)}', {p.Candidate1Id}, {p.Candidate2Id})");
        }

        var pairsInsertSQL =
        @$"insert into DoublePaymentPairs (Judgement, Score, Candidate1Id, Candidate2Id)
            values {string.Join(" ,", pairsInsertValues)}";

        affectedRows = await dbConnection.ExecuteAsync(pairsInsertSQL);
        Console.WriteLine($"Affected rows for pairs insert: {affectedRows}");

        Console.WriteLine();
        Console.WriteLine("Finished seeding database.");
    }
}