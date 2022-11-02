using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wiinf_Studie.API.Models;

public class DoublePaymentPair
{
    public int PairId { get; set; }
    public string Judgement { get; set; } = Judgments.NoSelection;
    public decimal Score { get; set; }
    public DoublePaymentCandidate Candidate1 { get; set; }
    public int Candidate1Id { get; set; }
    public DoublePaymentCandidate Candidate2 { get; set; }
    public int Candidate2Id { get; set; }
}

public class DoublePaymentCandidate
{
    public int CandidateId { get; set; }
    public string DocumentId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "EUR";
    public string DocumentType { get; set; }
    public string CompanyCode { get; set; }
    public string VendorNumber { get; set; }
    public string SAPClient { get; set; }
    public int FiscalYear { get; set; }
    public string PurchasingDocumentId { get; set; }
    public string TransactionCode { get; set; }
    public DateTime AccountingDate { get; set; }
    public DateTime DocumentDate { get; set; }
    public string Username { get; set; }
    public string ClearingDocumentId { get; set; }
    public DateTime ClearingDate { get; set; }
    // Todo: add other properties from Excel
}

public static class Judgments
{
    public const string NoSelection = "No selection";
    public const string UnderInspection = "Under inspection";
    public const string NoDuplicatePayment = "No Duplicate payment";
    public const string DuplicatePayment = "Duplicate payment";
    public const string CorrectedDuplicatePayment = "Corrected duplicate payment";
    public static readonly List<string> Allowed = new()
        { NoSelection, UnderInspection, NoDuplicatePayment, DuplicatePayment, CorrectedDuplicatePayment };
}

public static class Currencies
{
    public const string EUR = "EUR";
    public const string USD = "USD";
    public static readonly List<string> Allowed = new() { EUR, USD };
}

public static class DocumentTypes
{
    // Rechnung
    public const string Invoice = "Invoice";
    // Eingangsrechung
    public const string IncomingInvoice = "Incoming Invoice";
    public static readonly List<string> Allowed = new() { Invoice, IncomingInvoice };
}