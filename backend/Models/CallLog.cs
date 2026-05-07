namespace LoanSaas.Backend.Models;

public class CallLog
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int LeadId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string CallStatus { get; set; } = string.Empty;
    public string Outcome { get; set; } = string.Empty;
    public int DurationSeconds { get; set; }
    public decimal Cost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}