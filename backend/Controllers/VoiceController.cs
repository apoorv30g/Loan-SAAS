namespace LoanSaas.Backend.Controllers;
[ApiController]
[Route("voice")]
public class VoiceController : ControllerBase
{
    private readonly IConfiguration _config;

    public VoiceController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("{leadId}")]
    public IActionResult Get(int leadId)
    {
        var baseUrl = _config["BaseUrl"];

        var xml = $@"
<Response>
    <Play>{baseUrl}/audio/{leadId}</Play>
</Response>";

        return Content(xml, "text/xml");
    }
}