using Microsoft.AspNetCore.Mvc;
using LoanSaas.Backend.Services;

namespace LoanSaas.Backend.Controllers;

[ApiController]
[Route("test-voice")]
public class TestVoiceController : ControllerBase
{
    private readonly ElevenLabsService _eleven;

    public TestVoiceController(ElevenLabsService eleven)
    {
        _eleven = eleven;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var audio = await _eleven.TextToSpeech(
            "Hello, this is your AI loan assistant calling you."
        );

        return File(audio, "audio/mpeg");
    }
}
