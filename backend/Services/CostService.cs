namespace LoanSaas.Backend.Services;

using LoanSaas.Backend.Data;
using LoanSaas.Backend.Models;

public class CostService
{
    private readonly AppDbContext _db;

    public CostService(AppDbContext db)
    {
        _db = db;
    }

    public async Task LogCallAsync(
        Lead lead,
        string provider,
        string callStatus,
        string outcome,
        int durationSeconds,
        decimal cost,
        string type)
    {
        _db.ClientCosts.Add(new ClientCost
        {
            ClientId = lead.ClientId,
            LeadId = lead.Id,
            Type = type,
            Provider = provider,
            Cost = cost
        });

        _db.CallLogs.Add(new CallLog
        {
            ClientId = lead.ClientId,
            LeadId = lead.Id,
            Provider = provider,
            CallStatus = callStatus,
            Outcome = outcome,
            DurationSeconds = durationSeconds,
            Cost = cost
        });

        await _db.SaveChangesAsync();
    }

    public async Task LogSmsAsync(Lead lead, decimal cost)
    {
        _db.ClientCosts.Add(new ClientCost
        {
            ClientId = lead.ClientId,
            LeadId = lead.Id,
            Type = "sms",
            Provider = "twilio",
            Cost = cost
        });

        await _db.SaveChangesAsync();
    }
}