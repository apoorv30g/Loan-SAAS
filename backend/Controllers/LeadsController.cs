namespace LoanSaas.Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using LoanSaas.Backend.Data;


[ApiController]
[Route("leads")]
public class LeadsController : ControllerBase
{
    private readonly AppDbContext _db;

    public LeadsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetLeads([FromQuery] int clientId)
    {
        var leads = _db.Leads
            .Where(x => x.ClientId == clientId)
            .ToList();

        return Ok(leads);
    }
}