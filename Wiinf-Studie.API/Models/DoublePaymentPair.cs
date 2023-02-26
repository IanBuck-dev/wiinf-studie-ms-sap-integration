namespace Wiinf_Studie.API.Models;

#pragma warning disable CS8618
public class DoublePaymentPair
{
    public int PairId { get; set; }
    public string Judgement { get; set; } = Judgments.NoSelection;
    public decimal Score { get; set; }
    public DoublePaymentCandidate Candidate1 { get; set; }
    public int Candidate1Id { get; set; }
    public DoublePaymentCandidate Candidate2 { get; set; }
    public int Candidate2Id { get; set; }

    // Needed for bug fix in PowerApps with DataTables and nested properties support
    // Candidate 1
    public int CandidateId1 { get => Candidate1.CandidateId; }
    public string DocumentId1 { get => Candidate1.DocumentId; }
    public decimal Amount1 { get => Candidate1.Amount; }
    public string Currency1 { get => Candidate1.Currency; }
    public string DocumentType1 { get => Candidate1.DocumentType; }
    public string CompanyCode1 { get => Candidate1.CompanyCode; }
    public string VendorNumber1 { get => Candidate1.VendorNumber; }
    public string SAPClient1 { get => Candidate1.SAPClient; }
    public int FiscalYear1 { get => Candidate1.FiscalYear; }
    public string PurchasingDocumentId1 { get => Candidate1.PurchasingDocumentId; }
    public string TransactionCode1 { get => Candidate1.TransactionCode; }
    public DateTime AccountingDate1 { get => Candidate1.AccountingDate; }
    public DateTime DocumentDate1 { get => Candidate1.DocumentDate; }
    public string Username1 { get => Candidate1.Username; }
    public string ClearingDocumentId1 { get => Candidate1.ClearingDocumentId; }
    public DateTime ClearingDate1 { get => Candidate1.ClearingDate; }

    // Candidate 2
    public int CandidateId2 { get => Candidate2.CandidateId; }
    public string DocumentId2 { get => Candidate2.DocumentId; }
    public decimal Amount2 { get => Candidate2.Amount; }
    public string Currency2 { get => Candidate2.Currency; }
    public string DocumentType2 { get => Candidate2.DocumentType; }
    public string CompanyCode2 { get => Candidate2.CompanyCode; }
    public string VendorNumber2 { get => Candidate2.VendorNumber; }
    public string SAPClient2 { get => Candidate2.SAPClient; }
    public int FiscalYear2 { get => Candidate2.FiscalYear; }
    public string PurchasingDocumentId2 { get => Candidate2.PurchasingDocumentId; }
    public string TransactionCode2 { get => Candidate2.TransactionCode; }
    public DateTime AccountingDate2 { get => Candidate2.AccountingDate; }
    public DateTime DocumentDate2 { get => Candidate2.DocumentDate; }
    public string Username2 { get => Candidate2.Username; }
    public string ClearingDocumentId2 { get => Candidate2.ClearingDocumentId; }
    public DateTime ClearingDate2 { get => Candidate2.ClearingDate; }
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

#pragma warning restore CS8618