namespace LoanSaas.Backend.Models;

public class Lead
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int LoanAmount { get; set; }
    public string DropStage { get; set; } = string.Empty;
    public int Score { get; set; } = 0;
    public string Status { get; set; } = "pending";
    public int Attempts { get; set; } = 0;
    public string? LastCallOutcome { get; set; }  
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}