namespace LoanSaas.Backend.Models;

public class ClientCost
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int LeadId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
