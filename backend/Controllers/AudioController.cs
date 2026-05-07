namespace LoanSaas.Backend.Controllers;

[ApiController]
[Route("audio")]
public class AudioController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly ElevenLabsService _eleven;

    public AudioController(AppDbContext db, ElevenLabsService eleven)
    {
        _db = db;
        _eleven = eleven;
    }

    [HttpGet("{leadId}")]
    public async Task<IActionResult> Get(int leadId)
    {
        var lead = _db.Leads.Find(leadId);

        if (lead == null)
            return NotFound();

        var script = $@"
        Hi {lead.Name}, this is Maya from your loan provider.

        I noticed you were applying for around {lead.LoanAmount} rupees
        and got stuck at {lead.DropStage}.

        I just wanted to check if you faced any issue.
        ";

        var audio = await _eleven.TextToSpeech(script);

        return File(audio, "audio/mpeg");
    }
}