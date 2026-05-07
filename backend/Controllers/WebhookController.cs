using Microsoft.AspNetCore.Mvc;
using LoanSaas.Backend.Data;

namespace LoanSaas.Backend.Controllers;

[ApiController]
[Route("webhook")]
public class WebhookController : ControllerBase
{
    private readonly AppDbContext _db;

    public WebhookController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("elevenlabs")]
    public async Task<IActionResult> ReceiveElevenLabsWebhook([FromBody] ElevenLabsWebhookRequest request)
    {
        var lead = await _db.Leads.FindAsync(request.LeadId);
        if (lead is null)
        {
            return NotFound();
        }

        lead.Status = request.Status;
        lead.LastCallOutcome = request.Outcome;
        lead.Attempts += 1;

        await _db.SaveChangesAsync();
        return Ok();
    }

    public sealed class ElevenLabsWebhookRequest
    {
        public int LeadId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Outcome { get; set; } = string.Empty;
    }
}